using FluentValidation;
using Identity.Application.Common.Validators;

namespace Identity.Application.ApplicationUsers.Commands.ConfirmEmails
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.Token).NotEmpty().WithMessage("Token must not be empty");
        }
    }
}
