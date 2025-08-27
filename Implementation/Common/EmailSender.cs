using Application;
using Application.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Implementation.Common
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _settings;

        public EmailSender(EmailSettings settings)
        {
            _settings = settings;
        }

        public Task SendEmailConfirmRegistrationAsync(string email, string code)
        {
            Dictionary<string, string> placeholders = new Dictionary<string, string>
            {
                { "code", code }
            };

            var html = Extensions.LoadTemplateHtml("ConfirmRegistration.html", placeholders);
            return SendEmail(email, "TRAVILA - Confirm Registration", html);
        }

        public Task SendEmailForgotPasswordAsync(string email, string code)
        {
            Dictionary<string, string> placeholders = new Dictionary<string, string>
            {
                { "code", code }
            };

            var html = Extensions.LoadTemplateHtml("ForgotPassword.html", placeholders);
            return SendEmail(email, "TRAVILA - Forgot Password", html);
        }

        private async Task SendEmail(string emailTo, string subject, string body, bool isHtml = true)
        {
            var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_settings.FromEmail, _settings.Password)
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_settings.FromEmail, "TRAVILA Support"),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml,
            };

            mail.To.Add(emailTo);

            await client.SendMailAsync(mail);
        }
    }
}
