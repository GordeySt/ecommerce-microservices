using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace Identity.Grpc.Startup.Settings
{
    public class IdentityEmailSettings : IValidatable
    {
        [Required]
        public bool RequireConfirmation { get; set; }

        public bool RequiredUniqueEmail { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
