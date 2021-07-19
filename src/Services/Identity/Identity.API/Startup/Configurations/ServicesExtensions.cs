using Identity.API.Services;
using Identity.API.Startup.Settings;
using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Startup.Configurations
{
    public static class ServicesExtensions
    {
        public static void RegisterServices(this IServiceCollection services,
            AppSettings appSettings)
        {
            //AppSettings
            services.AddSingleton(appSettings);

            //Services
            services.AddScoped<IProfileService, ProfileService>();
        }
    }
}
