using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Startup.Settings
{
    public class CircuitBreakerSettings : IValidatable
    {
        [Required]
        public int HandledEventsAllowedBeforeBreaking { get; set; }

        [Required]
        public int DurationOfBreak { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
