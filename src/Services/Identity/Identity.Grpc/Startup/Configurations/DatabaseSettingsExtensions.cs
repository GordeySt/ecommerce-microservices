using Identity.Application.Common.Interfaces;
using Identity.Grpc.Startup.Settings;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Grpc.Startup.Configurations
{
    public static class DatabaseSettingsExtensions
    {
        public static void RegisterDatabase(this IServiceCollection services,
        AppSettings appSettings)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(appSettings.DbSettings.ConnectionString);
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }
    }
}
