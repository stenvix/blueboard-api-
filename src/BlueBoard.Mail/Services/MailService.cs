using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BlueBoard.Mail.Config;
using BlueBoard.Mail.Models;
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
            var message = new MailMessage()
            {
                From = new MailAddress(this.options.Email, this.options.Name),
                To = {mail.MailTo},
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
                client.Credentials = new NetworkCredential(this.options.Username, this.options.Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Timeout = 5000;

                client.Send(message);
            }
        }
    }
}
