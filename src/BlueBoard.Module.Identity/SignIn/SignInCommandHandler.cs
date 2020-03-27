using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using BlueBoard.Common;
using BlueBoard.Common.Enums;
using BlueBoard.Contract.Common;
using BlueBoard.Contract.Identity.Commands;
using BlueBoard.Contract.Identity.Models;
using BlueBoard.Mail.Models;
using BlueBoard.Mail.Services;
using BlueBoard.Module.Common.Exceptions;
using Dawn;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace BlueBoard.Module.Identity.SignIn
{
    public class SignInCommandHandler : AsyncRequestHandler<SignInCommand>
    {
        private readonly IMailService mailService;
        private readonly IMemoryCache memoryCache;

        public SignInCommandHandler(IMailService mailService, IMemoryCache memoryCache)
        {
            this.mailService = mailService;
            this.memoryCache = memoryCache;
        }

        protected override async Task Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //TODO: check if email exists
            if (false)
            {
                throw new BlueBoardException(ResponseCode.ValidationError, ErrorCodes.InvalidEmail);
            }

            var password = this.GetTempPassword();
            this.memoryCache.Set($"{Constants.Cache.SignKey}-{request.Email}", password);

            var mail = new MailModel
            {
                Text = $"Temporary password: {password}",
                MailTo = request.Email,
                Subject = "BlueBoard App"
            };

            await this.mailService.SendMailAsync(mail);
        }

        private string GetTempPassword()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
