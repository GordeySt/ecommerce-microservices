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
            var verifyUrl = $"{origin}/user/verifyEmail?token={token}&email={email}";

            var message = $"<p>Please click the below link to verify your email address:</p><p><a href='{verifyUrl}'>{verifyUrl}></a></p>";

            var subject = "Please verify your email address";

            await _emailSender.SendEmailAsync(email, subject, message);
        }

        public async Task SendResetPasswordEmail(string token, string origin, string email)
        {
            var resetPasswordUrl = $"{origin}/user/resetPassword?token={token}&email={email}";

            var message = $"<p>Please click the below link to reset your password</p><p><a href='{resetPasswordUrl}'>{resetPasswordUrl}></a></p>";

            var subject = "Reset password confirmation";

            await _emailSender.SendEmailAsync(email, subject, message);
        }
    }
}
