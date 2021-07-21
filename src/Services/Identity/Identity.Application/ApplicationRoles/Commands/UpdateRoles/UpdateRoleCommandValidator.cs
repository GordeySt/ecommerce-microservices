using FluentValidation;

namespace Identity.Application.ApplicationRoles.Commands.UpdateRoles
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Role name must not be empty");
        }
    }
}
