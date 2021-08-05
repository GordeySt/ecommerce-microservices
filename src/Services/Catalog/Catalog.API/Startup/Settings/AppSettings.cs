using NetEscapades.Configuration.Validation;

namespace Catalog.API.Startup.Settings
{
    public class AppSettings : IValidatable
    {
        public DbSettings DbSettings { get; set; }
        public CloudinarySettings CloudinarySettings { get; set; }
        public AppUrlsSettings AppUrlsSettings { get; set; }
        public CircuitBreakerSettings CircuitBreakerSettings { get; set; }
        public RetryPolicySettings RetryPolicySettings { get; set; }
        public RedisCacheSettings RedisCacheSettings { get; set; }

        public void Validate()
        {
            DbSettings.Validate();
            CloudinarySettings.Validate();
            AppUrlsSettings.Validate();
            CircuitBreakerSettings.Validate();
            RedisCacheSettings.Validate();
        }
    }
}
