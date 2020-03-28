using System.Threading;
using System.Threading.Tasks;
using BlueBoard.Common;
using BlueBoard.Common.Enums;
using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Mail.Models;
using BlueBoard.Mail.Services;
using BlueBoard.Module.Common.Exceptions;
using BlueBoard.Module.Identity.Helpers;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BlueBoard.Module.Identity.SignIn
{
    public class SignInCommandHandler : AsyncRequestHandler<SignInCommand>
    {
        private readonly IMailService mailService;
        private readonly IMemoryCache memoryCache;
        private readonly IConnectionFactory connectionFactory;
        private readonly IUserRepository userRepository;

        public SignInCommandHandler(IMailService mailService, IMemoryCache memoryCache,
            IConnectionFactory connectionFactory, IUserRepository userRepository)
        {
            this.mailService = mailService;
            this.memoryCache = memoryCache;
            this.connectionFactory = connectionFactory;
            this.userRepository = userRepository;
        }

        protected override async Task Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            using (var connection = this.connectionFactory.Create())
            {
                var exists = await this.userRepository.IsUserExistsAsync(connection, request.Email);
                if (!exists)
                {
                    throw new BlueBoardValidationException(ErrorCodes.InvalidEmail);
                }

                connection.Close();
            }

            var password = PasswordHelper.GeneratePassword();
            this.memoryCache.Set(PasswordHelper.GetCacheKey(request.Email), password);

            var mail = new MailModel(request.Email, "BlueBoard App", $"Temporary password: {password}");
            await this.mailService.SendMailAsync(mail);
        }
    }
}
