using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BlueBoard.Mail.Models;
using BlueBoard.Module.Mail.Config;
using Microsoft.Extensions.Configuration;

namespace BlueBoard.Mail.Services
{
    public class MailService : IMailService
    {
        private readonly MailOptions options;

        public MailService(IConfiguration configuration)
        {
            this.options = configuration.GetSection("Mail").Get<MailOptions>();
        }


        public Task SendMailAsync(MailModel mail)
        {
            var message = new MailMessage(this.options.Email, mail.MailTo)
            {
                Subject = mail.Subject,
                Body = mail.Text
            };

            this.SendMessage(message);
            return Task.CompletedTask;
        }

        private void SendMessage(MailMessage message)
        {
            using (var client = new SmtpClient(this.options.Host, this.options.Port))
            {
                client.Credentials = new NetworkCredential(this.options.Email, this.options.Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;

                client.Send(message);
            }
        }
    }
}
