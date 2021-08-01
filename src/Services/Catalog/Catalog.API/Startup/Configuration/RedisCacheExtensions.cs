using Catalog.API.Startup.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Startup.Configuration
{
    public static class RedisCacheExtensions
    {
        public static void RegisterRedisCache(this IServiceCollection services,
            AppSettings appSettings)
        {
            services.AddStackExchangeRedisCache
                (options => options.Configuration = appSettings.RedisCacheSettings.ConnectionString);
        }
    }
}
