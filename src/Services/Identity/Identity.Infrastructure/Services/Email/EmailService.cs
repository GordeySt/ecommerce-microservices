using Identity.Application.Common.Interfaces;
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
            var prefixRoute = "user/verifyemail";

            var verifyUrl = GetUrl(origin, prefixRoute, token, email);

            var messageTitle = "Click to verify email address";

            var message = $"<a href='{verifyUrl}'>{messageTitle}</a>";

            var subject = "Please verify your email address";

            await _emailSender.SendEmailAsync(email, subject, message);
        }

        public async Task SendResetPasswordEmail(string token, string origin, string email)
        {
            var prefixRoute = "user/resetpassword";

            var resetPasswordUrl = GetUrl(origin, prefixRoute, token, email);

            var messageTitle = "Click to reset password";

            var message = $"<a href='{resetPasswordUrl}'>{messageTitle}</a>";

            var subject = "Reset password confirmation";

            await _emailSender.SendEmailAsync(email, subject, message);
        }

        private static string GetUrl(string origin, string prefixRoute, string token, string email) =>
            $"{origin}/{prefixRoute}?token={token}&email={email}";
    }
}
