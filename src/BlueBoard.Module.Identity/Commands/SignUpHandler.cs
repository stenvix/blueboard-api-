using System.Threading;
using System.Threading.Tasks;
using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Commands;
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
    internal class SignUpHandler : AsyncRequestHandler<SignUp>
    {
        public SignUpHandler(
            IMailService mailService,
            IMemoryCache memoryCache,
            IUnitOfWorkFactory unitOfWorkFactory,
            IUserRepository userRepository)
        {
            this.MailService = mailService;
            this.MemoryCache = memoryCache;
            this.UnitOfWorkFactory = unitOfWorkFactory;
            this.UserRepository = userRepository;
        }

        private IMailService MailService { get; }
        private IMemoryCache MemoryCache { get; }
        private IUnitOfWorkFactory UnitOfWorkFactory { get; }
        private IUserRepository UserRepository { get; }

        protected override async Task Handle(SignUp request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = this.UnitOfWorkFactory.Create())
            {
                var exists = await this.UserRepository.IsUserExistsAsync(unitOfWork.Connection, request.Email);
                if (exists)
                {
                    throw new BlueBoardValidationException(ErrorCodes.EmailInUse);
                }

                await this.UserRepository.CreateUserAsync(unitOfWork.Connection, request.Email);

                unitOfWork.Commit();
            }


            var password = PasswordHelper.GeneratePassword();
            this.MemoryCache.Set(PasswordHelper.GetCacheKey(request.Email), password);

            var mail = new MailModel(request.Email, "BlueBoard App", $"Temporary password: {password}");
            await this.MailService.SendMailAsync(mail);
        }
    }
}
