using Identity.Application.Common.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string userEmail, string emailSubject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("", _config["smtpClientSettings:senderMailLogin"]));
            emailMessage.To.Add(new MailboxAddress("", userEmail));
            emailMessage.Subject = emailSubject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(_config["smtpClientSettings:host"], 587, false);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_config["smtpClientSettings:senderMailLogin"],
                _config["smtpClientSettings:senderMailPassword"]);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
