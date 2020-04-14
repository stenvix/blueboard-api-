using System;
using System.Linq;
using BlueBoard.Module.Common;
using BlueBoard.Persistence.Abstractions;
using BlueBoard.Persistence.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace BlueBoard.API.Helpers
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConnectionFactory connectionFactory;
        private readonly IUserRepository userRepository;
        private Lazy<string> emailLazy;

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor, IConnectionFactory connectionFactory,
            IUserRepository userRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.connectionFactory = connectionFactory;
            this.userRepository = userRepository;
            this.emailLazy = new Lazy<string>(this.GetEmail);
        }

        public long UserId => this.GetId();
        public string Email => this.emailLazy.Value;

        private long GetId()
        {
            if (long.TryParse(this.httpContextAccessor.HttpContext.User.Identity.Name, out var id))
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

            using (var connection = this.connectionFactory.Create())
            {
                var user = this.userRepository.FindById(connection, id).GetAwaiter().GetResult();
                return user.Email;
            }
        }
    }
}
