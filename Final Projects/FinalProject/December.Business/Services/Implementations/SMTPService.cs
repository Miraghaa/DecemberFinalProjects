using December.Business.Contacts.Email;
using December.Business.Options;
using December.Business.Services.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
namespace December.Business.Services.Implementations
{
    public class SMTPService : IEmailService
    {
        private EmailConfigOptions _emailConfig;

        public SMTPService(IOptions<EmailConfigOptions> emailConfigOptions)
        {
            _emailConfig = emailConfigOptions.Value;
        }

        public void Send(MessageVM message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(MessageVM message)
        {
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message.Content;


            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(string.Empty, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Send(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmptServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(emailMessage);
                }
                catch
                {

                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
