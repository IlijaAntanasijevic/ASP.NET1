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
            var html = BuildConfirmationEmail(code);
            return SendEmail(email, "TRAVILA - Confirm Registration", html);
        }

        public Task SendEmailForgotPasswordAsync(string email, string code)
        {
            var html = BuildForgotPasswordEmail(code);
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

        #region Build HTML
        private string BuildConfirmationEmail(string confirmationCode)
        {
            return $@"
                    <html>
                      <head>
                        <style>
                          body {{
                            font-family: Arial, sans-serif;
                            background-color: #f9f9f9;
                            padding: 20px;
                            color: #333;
                          }}
                          .container {{
                            max-width: 600px;
                            margin: auto;
                            background: #fff;
                            border-radius: 8px;
                            box-shadow: 0 2px 6px rgba(0,0,0,0.1);
                            padding: 20px;
                          }}
                          h2 {{
                            color: #171717;
                          }}
                          .code {{
                            font-size: 24px;
                            font-weight: bold;
                            background: #f1f1f1;
                            padding: 10px 20px;
                            border-radius: 6px;
                            display: inline-block;
                            letter-spacing: 3px;
                            margin: 20px 0;
                          }}
                          p {{
                            font-size: 15px;
                          }}
                        </style>
                      </head>
                      <body>
                        <div class='container'>
                          <h2>Confirm Your Registration</h2>
                          <p>Thank you for signing up with <strong>TRAVILA</strong>!</p>
                          <p>Please use the confirmation code below to complete your registration:</p>
                          <div class='code'>{confirmationCode}</div>
                          <p>If you didn’t request this, please ignore this email.</p>
                          <p>Best regards,<br/>TRAVILA Team</p>
                        </div>
                      </body>
                    </html>";
        }
    
        private string BuildForgotPasswordEmail(string confirmtationCode)
        {
            return $@"
                <html>
                  <head>
                    <style>
                      body {{
                        font - family: Arial, sans-serif;
                        background-color: #f9f9f9;
                        padding: 20px;
                        color: #333;
                      }}
                      .container {{max - width: 600px;
                        margin: auto;
                        background: #fff;
                        border-radius: 8px;
                        box-shadow: 0 2px 6px rgba(0,0,0,0.1);
                        padding: 20px;
                      }}
                      h2 {{
                        color: #171717;
                        }}
                      .code {{
                        font - size: 24px;
                        font-weight: bold;
                        background: #f1f1f1;
                        padding: 10px 20px;
                        border-radius: 6px;
                        display: inline-block;
                        letter-spacing: 3px;
                        margin: 20px 0;
                      }}
                      p {{
                        font - size: 15px;
                      }}
                    </style>
                  </head>
                  <body>
                    <div class='container'>
                      <h2>Reset Your Password</h2>
                      <p>We received a request to reset your password for<strong>TRAVILA</strong>.</p>
                      <p>Please use the code below to complete your password reset:</p>
                      <div class='code'>{confirmtationCode}</div>
                      <p>If you didn’t request this, please ignore this email.</p>
                      <p>Best regards,<br/>TRAVILA Team</p>
                    </div>
                  </body>
                </html>
            ";
        }

        #endregion
    }
}
