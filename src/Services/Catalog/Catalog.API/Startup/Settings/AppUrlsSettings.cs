using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Startup.Settings
{
    public class AppUrlsSettings : IValidatable
    {
        [Required]
        public string IdentityUrl { get; set; }

        [Required]
        public string CatalogUrl { get; set; }

        [Required]
        public string IdentityGrpcUrl { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
