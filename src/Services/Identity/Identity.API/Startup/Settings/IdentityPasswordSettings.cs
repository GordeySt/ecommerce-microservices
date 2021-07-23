using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.Startup.Settings
{
    public class IdentityPasswordSettings : IValidatable
    {
        [Required]
        public bool RequireDigit { get; set; }

        [Required]
        public bool RequireLowercase { get; set; }

        [Required]
        public bool RequireUppercase { get; set; }

        [Required]
        public bool RequireNonAlphanumeric { get; set; }

        [Required]
        public int RequiredUniqueChars { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
