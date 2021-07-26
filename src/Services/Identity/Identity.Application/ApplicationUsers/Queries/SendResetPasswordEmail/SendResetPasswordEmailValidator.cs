using FluentValidation;
using Identity.Application.Common.Validators;

namespace Identity.Application.ApplicationUsers.Queries.SendResetPasswordEmail
{
    public class SendResetPasswordEmailValidator : AbstractValidator<SendResetPasswordEmailQuery>
    {
        public SendResetPasswordEmailValidator()
        {
            RuleFor(v => v.Email).Email();
        }
    }
}
