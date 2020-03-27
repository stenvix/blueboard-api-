using System;
using System.Threading.Tasks;
using BlueBoard.Mail.Models;

namespace BlueBoard.Mail.Services
{
    public interface IMailService
    {
        Task SendMailAsync(MailModel mail);
    }
}
