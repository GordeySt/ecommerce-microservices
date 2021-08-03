using NetEscapades.Configuration.Validation;
using System.ComponentModel.DataAnnotations;

namespace Basket.API.Startup.Settings
{
    public class MongoDbSettings : IValidatable
    {
        [Required]
        public string Host { get; set; }

        [Required]
        public int Port { get; set; }

        [Required]
        public string DatabaseName { get; set; }

        [Required]
        public string OrdersCollectionName { get; set; }

        public string ConnectionString => $"mongodb://{Host}:{Port}";
        
        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }
    }
}
