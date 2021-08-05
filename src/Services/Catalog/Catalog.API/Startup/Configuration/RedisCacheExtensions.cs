using Catalog.API.Startup.Settings;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Catalog.API.Startup.Configuration
{
    public static class RedisCacheExtensions
    {
        public static void RegisterRedisCache(this IServiceCollection services,
            AppSettings appSettings)
        {
            var redisConfiguration = new RedisConfiguration
            {
                AllowAdmin = appSettings.RedisCacheSettings.AllowAdmin,
                Hosts = new[]
                {
                    new RedisHost
                    {
                        Host = appSettings.RedisCacheSettings.Host,
                        Port = appSettings.RedisCacheSettings.Port
                    }
                }
            };

            services
                .AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);
        }
    }
}
