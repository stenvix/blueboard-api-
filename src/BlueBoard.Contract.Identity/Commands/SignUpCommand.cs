using MediatR;

namespace BlueBoard.Contract.Identity.Commands
{
    public class SignUpCommand: IRequest
    {
        public SignUpCommand(string email)
        {
            this.Email = email;
        }

        public string Email { get; }
    }
}
