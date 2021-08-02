using Catalog.API.Startup.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Startup.Configuration
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
