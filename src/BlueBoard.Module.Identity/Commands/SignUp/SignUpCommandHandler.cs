using System.Threading;
using System.Threading.Tasks;
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

namespace BlueBoard.Module.Identity.Commands.SignUp
{
    public class SignUpCommandHandler : AsyncRequestHandler<SignUpCommand>
    {
        private readonly IMailService mailService;
        private readonly IMemoryCache memoryCache;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IUserRepository userRepository;

        public SignUpCommandHandler(IMailService mailService, IMemoryCache memoryCache,
            IUnitOfWorkFactory unitOfWorkFactory, IUserRepository userRepository)
        {
            this.mailService = mailService;
            this.memoryCache = memoryCache;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.userRepository = userRepository;
        }

        protected override async Task Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = this.unitOfWorkFactory.Create())
            {
                var exists = await this.userRepository.IsUserExistsAsync(unitOfWork.Connection, request.Email);
                if (exists)
                {
                    throw new BlueBoardValidationException(ErrorCodes.EmailInUse);
                }

                await this.userRepository.CreateUserAsync(unitOfWork.Connection, request.Email);

                unitOfWork.Commit();
            }


            var password = PasswordHelper.GeneratePassword();
            this.memoryCache.Set(PasswordHelper.GetCacheKey(request.Email), password);

            var mail = new MailModel(request.Email, "BlueBoard App", $"Temporary password: {password}");
#pragma warning disable 4014
            Task.Run(() => { this.mailService.SendMailAsync(mail); }, cancellationToken);
#pragma warning restore 4014
        }
    }
}
