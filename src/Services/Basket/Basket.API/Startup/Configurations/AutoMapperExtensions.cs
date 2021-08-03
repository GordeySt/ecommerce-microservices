using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Basket.API.Startup.Configurations
{
    public static class AutoMapperExtensions
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
