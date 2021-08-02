using Basket.API.Startup.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Basket.API.Startup.Configurations
{
    public static class HealthChecksExtensions
    {
        public static void RegisterHealthChecks(this IServiceCollection services,
            AppSettings appSettings)
        {
            services.AddHealthChecks()
                .AddRedis(
                appSettings.RedisCacheSettings.ConnectionString,
                "Redis Health",
                HealthStatus.Degraded);
               
        }
    }
}
