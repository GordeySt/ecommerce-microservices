using Identity.API.Startup.Settings;
using Identity.Application.Common.Interfaces;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Startup.Configurations
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

            services.AddScoped<IApplicationDbContext>
                (provider => provider.GetService<ApplicationDbContext>());
        }
    }
}
