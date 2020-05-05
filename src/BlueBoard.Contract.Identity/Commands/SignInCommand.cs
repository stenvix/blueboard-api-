using BlueBoard.Contract.Identity.Models;
using MediatR;

namespace BlueBoard.Contract.Identity.Commands
{
    public class SignInCommand : IRequest
    {
        public SignInCommand(string email)
        {
            this.Email = email;
        }

        public string Email { get; }
    }
}
