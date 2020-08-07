using System.Threading;
using System.Threading.Tasks;
using BlueBoard.Contract.Common;
using BlueBoard.Mail.Models;
using BlueBoard.Mail.Services;
using BlueBoard.Module.Common.Exceptions;
using BlueBoard.Module.Identity.Helpers;
using BlueBoard.Module.Identity.Repositories;
using BlueBoard.Persistence.Abstractions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BlueBoard.Module.Identity.Commands
{
    internal class SignInHandler : AsyncRequestHandler<Contract.Identity.Commands.SignIn>
    {


        public SignInHandler(
            IMailService mailService,
            IMemoryCache memoryCache,
            IConnectionFactory connectionFactory,
            IUserRepository userRepository)
        {
            this.MailService = mailService;
            this.MemoryCache = memoryCache;
            this.ConnectionFactory = connectionFactory;
            this.UserRepository = userRepository;
        }
        public IMailService MailService { get; }
        public IMemoryCache MemoryCache { get; }
        public IConnectionFactory ConnectionFactory { get; }
        public IUserRepository UserRepository { get; }
        protected override async Task Handle(Contract.Identity.Commands.SignIn request, CancellationToken cancellationToken)
        {
            using (var connection = this.ConnectionFactory.Create())
            {
                var exists = await this.UserRepository.IsUserExistsAsync(connection, request.Email);
                if (!exists)
                {
                    throw new BlueBoardValidationException(ErrorCodes.NotFound);
                }

                connection.Close();
            }

            var password = PasswordHelper.GeneratePassword();
            this.MemoryCache.Set(PasswordHelper.GetCacheKey(request.Email), password);

            var mail = new MailModel(request.Email, "BlueBoard App", $"Temporary password: {password}");
            await this.MailService.SendMailAsync(mail);
        }
    }
}
