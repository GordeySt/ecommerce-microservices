using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Startup.Settings
{
    public class RedisCacheSettings : IValidatable
    {
        [Required]
        public int Port { get; set; }

        [Required]
        public string Host { get; set; }

        public string ConnectionString => $"{Host}:{Port}";

        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
