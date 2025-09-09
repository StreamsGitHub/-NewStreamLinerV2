using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace StreamLinerLogicLayer.Helper.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlMessage)
        {
            var smtpSettings = _config.GetSection("Smtp");

            using var client = new SmtpClient
            {
                Host = smtpSettings["Host"],
                Port = int.Parse(smtpSettings["Port"]),
                EnableSsl = bool.Parse(smtpSettings["EnableSsl"] ?? "true"),
                Credentials = new NetworkCredential(
                    smtpSettings["Username"],
                    smtpSettings["Password"])
            };

            var mail = new MailMessage
            {
                From = new MailAddress(smtpSettings["From"]),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mail.To.Add(to);
            await client.SendMailAsync(mail);
        }
    }
}
