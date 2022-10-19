using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace EPharmacy.ServerApp.Services.MailSender
{
    public class MailSenderService : IMailSenderService
    {
        private readonly SendGridOptions _options;

        public MailSenderService(IOptions<SendGridOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendEmail(List<EmailAddress> emailTo, string subject, string emailBody)
        {
            var client = new SendGridClient(_options.ApiKey);            
            
            var plainTextBody = Regex.Replace(emailBody, @"<[^>]*>", string.Empty);
            var message = MailHelper.CreateSingleEmailToMultipleRecipients(new EmailAddress(_options.MailFrom, _options.NameFrom), emailTo, subject,
                plainTextBody, emailBody);                        

           var response = await client.SendEmailAsync(message);
           if (response.StatusCode != HttpStatusCode.Accepted)
               throw new Exception($"Cannot send email via send grid, status code: {response.StatusCode}");
        }
    }
}