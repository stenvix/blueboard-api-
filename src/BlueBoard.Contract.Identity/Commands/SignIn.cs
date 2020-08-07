using MediatR;

namespace BlueBoard.Contract.Identity.Commands
{
    public class SignIn : IRequest
    {
        public SignIn(string email)
        {
            this.Email = email;
        }

        public string Email { get; }
    }
}
