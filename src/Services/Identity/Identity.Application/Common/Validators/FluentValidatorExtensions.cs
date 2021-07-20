using FluentValidation;

namespace Identity.Application.Common.Validators
{
    public static class FluentValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
               .NotEmpty().WithMessage("Password must not be empty")
               .Matches("[a-z]").WithMessage("Password must have at least 1 lowercase character")
               .Matches("[0-9]").WithMessage("Password must contain a number");

            return options;
        }

        public static IRuleBuilder<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage("Email must not be empty")
                .EmailAddress().WithMessage("Invalid Email address");

            return options;
        }
    }
}
