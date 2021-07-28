using Identity.Grpc.Startup.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Grpc.Startup.Configurations
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
