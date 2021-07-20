using FluentValidation;
using Identity.Application.Common.Validators;

namespace Identity.Application.ApplicationUsers.Queries.ResendEmailVerifications
{
    public class ResendEmailVerificationQueryValidator : AbstractValidator<ResendEmailVerificationQuery>
    {
        public ResendEmailVerificationQueryValidator()
        {
            RuleFor(v => v.Email).Email();
        }
    }
}
