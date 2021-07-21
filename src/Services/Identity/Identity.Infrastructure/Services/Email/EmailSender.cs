using Identity.Application.Common.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<SmtpClientSettings> _config;

        public EmailSender(IOptions<SmtpClientSettings> config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string userEmail, string emailSubject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("", _config.Value.SenderEmailLogin));
            emailMessage.To.Add(new MailboxAddress("", userEmail));
            emailMessage.Subject = emailSubject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(_config.Value.Host, 587, false);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_config.Value.SenderEmailLogin,
                _config.Value.SenderEmailPassword);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
