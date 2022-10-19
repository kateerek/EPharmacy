using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;

namespace EPharmacy.ServerApp.Services.MailSender
{
    public interface IMailSenderService
    {
        Task SendEmail(List<EmailAddress> emailTo, string subject, string emailBody);
    }
}