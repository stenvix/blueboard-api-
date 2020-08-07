using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Commands
{
    public class VerifyAccess : IRequest<AccessTokenModel>
    {
        public VerifyAccess(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
