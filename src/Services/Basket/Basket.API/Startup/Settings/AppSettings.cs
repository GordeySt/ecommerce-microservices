using NetEscapades.Configuration.Validation;

namespace Basket.API.Startup.Settings
{
    public class AppSettings : IValidatable
    {
        public AppUrlsSettings AppUrlsSettings { get; set; }

        public RedisCacheSettings RedisCacheSettings { get; set; }

        public void Validate()
        {
            AppUrlsSettings.Validate();
            RedisCacheSettings.Validate();
        }
    }
}
