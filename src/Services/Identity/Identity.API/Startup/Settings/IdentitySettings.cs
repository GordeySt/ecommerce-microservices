using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.Startup.Settings
{
    public class IdentitySettings : IValidatable
    {
        public IdentityEmailSettings Email { get; set; }
        public IdentityPasswordSettings Password { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);

            Password.Validate();
            Email.Validate();
        }
    }
}
