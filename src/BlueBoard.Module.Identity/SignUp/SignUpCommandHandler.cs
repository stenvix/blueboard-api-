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
using BlueBoard.Persistence.Abstractions.Entities;
using BlueBoard.Persistence.Abstractions.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BlueBoard.Module.Identity.SignUp
{
    public class SignUpCommandHandler : AsyncRequestHandler<SignUpCommand>
    {
        private readonly IMailService mailService;
        private readonly IMemoryCache memoryCache;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IUserRepository userRepository;

        public SignUpCommandHandler(IMailService mailService, IMemoryCache memoryCache, IUnitOfWorkFactory unitOfWorkFactory, IUserRepository userRepository)
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
                var exists = this.userRepository.IsUserExists(unitOfWork.Connection, request.Email);
                if (exists)
                {
                    throw new BlueBoardValidationException(ErrorCodes.EmailInUse);
                }

                var result = this.userRepository.CreateUser(unitOfWork.Connection, request.Email);

                unitOfWork.Commit();
            }


            var password = PasswordHelper.GeneratePassword();
            this.memoryCache.Set($"{Constants.Cache.SignKey}-{request.Email}", password);

            var mail = new MailModel(request.Email, "BlueBoard App", $"Temporary password: {password}");
            await this.mailService.SendMailAsync(mail);
        }
    }
}
