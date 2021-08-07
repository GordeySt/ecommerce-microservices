using Basket.API.Startup.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.API.Startup.Configurations
{
    public static class HealthChecksExtensions
    {
        public static void RegisterHealthChecks(this IServiceCollection services,
            AppSettings appSettings)
        {
            services.AddHealthChecks()
                .AddRedis(appSettings.RedisCacheSettings.ConnectionString)
                .AddMongoDb(appSettings.MongoDbSettings.ConnectionString);
        }
    }
}
