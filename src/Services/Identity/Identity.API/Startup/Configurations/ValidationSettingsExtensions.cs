using Identity.API.Startup.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Startup.Configurations
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
