using Identity.Application.Common.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendEmailVerificationAsync(string token, string origin, string email)
        {
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var verifyUrl = $"{origin}/user/verifyEmail?token={token}&email={email}";

            var message = $"<p>Please click the below link to verify your email address:</p><p><a href='{verifyUrl}'>{verifyUrl}></a></p>";

            var subject = "Please verify your email address";

            await _emailSender.SendEmailAsync(email, subject, message);
        }
    }
}
