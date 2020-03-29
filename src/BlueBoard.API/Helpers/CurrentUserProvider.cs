using System.Linq;
using BlueBoard.Module.Common;
using Microsoft.AspNetCore.Http;

namespace BlueBoard.API.Helpers
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int UserId => this.GetId();

        private int GetId()
        {
            if (int.TryParse(this.httpContextAccessor.HttpContext.User.Identity.Name, out var id))
            {
                return id;
            }

            return -1;
        }
    }
}
