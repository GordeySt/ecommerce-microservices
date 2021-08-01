using Catalog.API.BL.Interfaces;
using Catalog.API.BL.Services;
using Catalog.API.BL.Services.CloudinaryService;
using Catalog.API.BL.Services.ResponseCaching;
using Catalog.API.DAL;
using Catalog.API.DAL.Interfaces;
using Catalog.API.DAL.Repositories;
using Catalog.API.Startup.Settings;
using Common.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Startup.Configuration
{
    public static class ServicesExtensions
    {
        public static void RegisterServices(this IServiceCollection services,
            AppSettings appSettings)
        {
            //AppSettings
            services.AddSingleton(appSettings);

            services.AddHttpContextAccessor();

            //Services
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IPhotoCloudAccessor, PhotoCloudAccessor>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IProductRatingsService, ProductRatingsService>();

            //Repository
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IProductRatingsRepository, ProductRatingsRepository>();

            services.AddTransient<LoggingDelegatingHandler>();
        }
    }
}
