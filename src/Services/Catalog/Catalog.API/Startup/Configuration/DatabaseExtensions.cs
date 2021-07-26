using Catalog.API.DAL;
using Catalog.API.Startup.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Startup.Configuration
{
    public static class DatabaseExtensions
    {
        public static void RegisterDatabase(this IServiceCollection services,
        AppSettings appSettings)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(appSettings.DbSettings.ConnectionString);
            });
        }
    }
}
