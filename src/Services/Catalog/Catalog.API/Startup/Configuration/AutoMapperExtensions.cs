using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.API.Startup.Configuration
{
    public static class AutoMapperExtensions
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
