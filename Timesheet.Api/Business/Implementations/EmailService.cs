using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;

namespace Timesheet.Api.Business.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettingsDto _settings;

        public EmailService(IOptions<EmailSettingsDto> settings)
        {
            _settings = settings.Value;
        }

        public void SendEmail(string toEmail, string subject, string message)
        {
            toEmail = "jsergiobarbosa62@gmail.com";

            try
            {
                using (var client = new System.Net.Mail.SmtpClient())
                {
                    client.Credentials = new NetworkCredential
                    {
                        UserName = _settings.Username,
                        Password = _settings.Password
                    };

                    client.Host = _settings.Host;
                    client.Port = _settings.Port;
                    client.EnableSsl = false;

                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(new MailAddress(toEmail));
                        emailMessage.From = new MailAddress(_settings.From);
                        emailMessage.Subject = subject;
                        emailMessage.IsBodyHtml = true;
                        emailMessage.Body = message;
                        client.Send(emailMessage);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
    }


}
