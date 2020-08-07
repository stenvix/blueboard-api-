using MediatR;

namespace BlueBoard.Contract.Identity.Commands
{
    public class SignUp: IRequest
    {
        public SignUp(string email)
        {
            this.Email = email;
        }

        public string Email { get; }
    }
}
