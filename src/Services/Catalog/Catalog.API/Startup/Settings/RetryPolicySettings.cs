using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Startup.Settings
{
    public class RetryPolicySettings : IValidatable
    {
        [Required]
        public int RetryCount { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
