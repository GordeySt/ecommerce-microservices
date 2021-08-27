using FluentValidation;
using Identity.Application.Common.Validators;

namespace Identity.Application.ApplicationUsers.Queries.SignInUsers
{
    public class SignInUsersQueryValidator : AbstractValidator<SignInUsersQuery>
    {
        public SignInUsersQueryValidator()
        {
            RuleFor(v => v.Email).Email();
            RuleFor(v => v.Password).Password();
        }
    }
}
