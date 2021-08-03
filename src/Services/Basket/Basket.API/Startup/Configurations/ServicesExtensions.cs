using Basket.API.BL.Interfaces;
using Basket.API.BL.Services;
using Basket.API.DAL;
using Basket.API.DAL.Interfaces.Mongo;
using Basket.API.DAL.Interfaces.Redis;
using Basket.API.DAL.Repositories.Redis;
using Basket.API.Startup.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.API.Startup.Configurations
{
    public static class ServicesExtensions
    {
        public static void RegisterServices(this IServiceCollection services,
            AppSettings appSettings)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IDatabaseContext, DatabaseContext>();

            // Settings
            services.AddSingleton(appSettings);

            // Repositories
            services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();

            // Services
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
            
        }
    }
}
