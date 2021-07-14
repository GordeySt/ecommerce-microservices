using Catalog.API.Startup.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Startup.Configuration
{
    public static class ValidationSettingsExtensions
    {
        public static void ValidateSettingParameters(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.UseConfigurationValidation();
            services.ConfigureValidatableSetting<AppSettings>(configuration);
        }
    }
}
