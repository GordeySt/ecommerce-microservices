using Catalog.API.BL.Interfaces;
using Catalog.API.BL.Services;
using Catalog.API.DAL;
using Catalog.API.DAL.Interfaces;
using Catalog.API.DAL.Repositories;
using Catalog.API.Startup.Settings;
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

            //Database context
            services.AddScoped<IDatabaseContext, DatabaseContext>();

            //Services
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<IPhotoCloudAccessor, PhotoCloudAccessor>();
            services.AddTransient<IPhotoService, PhotoService>();

            //Repository
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}
