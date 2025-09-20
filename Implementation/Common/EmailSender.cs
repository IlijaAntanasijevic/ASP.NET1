using Application;
using Application.Common;
using Application.DTO.Bookings;
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


        public Task ConfirmRegistrationAsync(string email, string code)
        {
            Dictionary<string, string> placeholders = new Dictionary<string, string>
            {
                { "code", code }
            };

            var html = Extensions.LoadTemplateHtml("ConfirmRegistration.html", placeholders);
            return SendEmail(email, "TRAVILA - Confirm Registration", html);
        }

        public Task ForgotPasswordAsync(string email, string code)
        {
            Dictionary<string, string> placeholders = new Dictionary<string, string>
            {
                { "code", code }
            };

            var html = Extensions.LoadTemplateHtml("ForgotPassword.html", placeholders);
            return SendEmail(email, "TRAVILA - Forgot Password", html);
        }

        public Task BookingConfirmed(ConfirmedBookingEmailDto data)
        {
            Dictionary<string, string> placeholders = new Dictionary<string, string>
            {
                { "checkIn", data.CheckIn },
                { "checkOut", data.CheckOut },
                { "adults", data.Adults },
                { "childrens", data.Childrens },
                { "pricePerNight", data.PricePerNight },
                { "totalPrice", data.TotalPrice },
                { "address", data.Address },
                { "userName", data.UserName },
                { "userLastName", data.UserLastName },
                { "userPhone", data.UserPhone },
                { "ownerName", data.OwnerName },
                { "ownerLastName", data.OwnerLastName },
                { "ownerPhone", data.OwnerPhone },
                { "ownerEmail", data.OwnerEmail }
            };

            var html = Extensions.LoadTemplateHtml("BookingTemplate.html", placeholders);
            return SendEmail(data.Email, "TRAVILA - Booking Confirmed", html);
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

            //mail.To.Add(emailTo);
            mail.To.Add("ilija.antanasijevic.48.21@ict.edu.rs");

            await client.SendMailAsync(mail);
        }
    }
}
