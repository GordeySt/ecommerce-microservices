using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace Basket.API.Startup.Settings
{
    public class AppUrlsSettings : IValidatable
    {
        [Required]
        public string IdentityUrl { get; set; }

        [Required]
        public string BasketUrl { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
