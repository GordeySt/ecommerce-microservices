using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace OcelotApiGateway.Settings
{
    public class AppUrlsSettings : IValidatable
    {
        [Required]
        public string IdentityUrl { get; set; }

        [Required]
        public string ClientUrl { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
