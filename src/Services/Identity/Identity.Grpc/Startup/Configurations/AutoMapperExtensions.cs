using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Identity.Grpc.Startup.Configurations
{
    public static class AutoMapperExtensions
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
