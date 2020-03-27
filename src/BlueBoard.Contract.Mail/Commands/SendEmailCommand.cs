using MediatR;

namespace BlueBoard.Contract.Mail.Commands
{
    public class SendEmailCommand : IRequest
    {
        public SendEmailCommand(string email, string text)
        {
            this.Email = email;
            this.Text = text;
        }

        public string Email { get; }

        public string Text { get; }
    }
}
