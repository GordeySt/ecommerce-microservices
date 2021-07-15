using Catalog.API.Startup.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Catalog.API.Startup.Configuration
{
    public static class HealthChecksExtensions
    {
        public static void RegisterHealthChecks(this IServiceCollection services,
            AppSettings appSettings)
        {
            services.AddHealthChecks()
                .AddMongoDb(
                    appSettings.DbSettings.ConnectionString,
                    "Catalog MongoDB Health",
                    HealthStatus.Degraded);
        }
    }
}
