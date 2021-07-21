using FluentValidation;
using Identity.Application.Common.Validators;

namespace Identity.Application.ApplicationUsers.Commands.ResetPasswords
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.Password).Password()
                .Equal(x => x.ConfirmPassword).WithMessage("Passwords should be equal");
            RuleFor(x => x.ConfirmPassword).Password();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
