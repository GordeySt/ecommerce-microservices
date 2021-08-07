using Identity.API.Startup.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Startup.Configurations
{
    public static class HealthChecksExtensions
    {
        public static void RegisterHealthChecks(this IServiceCollection services,
            AppSettings appSettings)
        {
            services.AddHealthChecks()
                .AddNpgSql(appSettings.DbSettings.ConnectionString);
        }
    }
}
