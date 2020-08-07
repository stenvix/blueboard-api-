using System;
using System.Linq;
using BlueBoard.Contract.Identity.Queries;
using BlueBoard.Module.Common;
using BlueBoard.Persistence.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlueBoard.API.Helpers
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private Lazy<string> emailLazy;

        public CurrentUserProvider(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor)
        {
            this.Mediator = mediator;
            this.HttpContextAccessor = httpContextAccessor;
            this.emailLazy = new Lazy<string>(this.GetEmail);
        }

        public long UserId => this.GetId();
        public string Email => this.emailLazy.Value;

        private IMediator Mediator { get; }
        private IHttpContextAccessor HttpContextAccessor { get; }

        private long GetId()
        {
            if (long.TryParse(this.HttpContextAccessor.HttpContext.User.Identity.Name, out var id))
            {
                return id;
            }

            return -1;
        }

        private string GetEmail()
        {
            var id = this.GetId();
            if (id == -1)
            {
                return string.Empty;
            }

            var user = this.Mediator.Send(new GetUser(id)).GetAwaiter().GetResult();
            return user.Email;
        }
    }
}
