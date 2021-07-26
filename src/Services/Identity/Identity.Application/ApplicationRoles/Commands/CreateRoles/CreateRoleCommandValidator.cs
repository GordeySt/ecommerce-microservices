using FluentValidation;

namespace Identity.Application.ApplicationRoles.Commands.CreateRoles
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Role name must not be empty");
        }
    }
}
