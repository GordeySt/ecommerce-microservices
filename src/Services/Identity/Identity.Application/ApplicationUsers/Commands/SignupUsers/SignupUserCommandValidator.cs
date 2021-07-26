using FluentValidation;
using Identity.Application.Common.Validators;

namespace Identity.Application.ApplicationUsers.Commands.SignupUsers
{
    public class SignupUserCommandValidator : AbstractValidator<SignupUserCommand>
    {
        public SignupUserCommandValidator()
        {
            RuleFor(v => v.Email).Email();
            RuleFor(v => v.Password).Password();
        }
    }
}
