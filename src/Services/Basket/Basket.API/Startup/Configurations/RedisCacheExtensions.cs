using Basket.API.Startup.Settings;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Basket.API.Startup.Configurations
{
    public static class RedisCacheExtensions
    {
        public static void RegisterRedisCache(this IServiceCollection services,
            AppSettings appSettings)
        {
            var redisConfiguration = new RedisConfiguration
            {
                Hosts = new[]
                {
                    new RedisHost
                    {
                        Host = appSettings.RedisCacheSettings.Host,
                        Port = appSettings.RedisCacheSettings.Port
                    }
                },
                Database = appSettings.RedisCacheSettings.Database
            };

            services
                .AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);
        }
    }
}
