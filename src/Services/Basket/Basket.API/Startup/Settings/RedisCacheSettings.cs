using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace Basket.API.Startup.Settings
{
    public class RedisCacheSettings : IValidatable
    {
        [Required]
        public int Port { get; set; }

        [Required]
        public string Host { get; set; }

        [Required]
        public int Database { get; set; }

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
