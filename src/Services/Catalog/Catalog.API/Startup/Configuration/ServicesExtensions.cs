using Catalog.API.BL.Interface;
using Catalog.API.BL.Services;
using Catalog.API.DAL;
using Catalog.API.DAL.Interfaces;
using Catalog.API.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Startup.Configuration
{
    public static class ServicesExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Database context
            services.AddScoped<IDatabaseContext, DatabaseContext>();

            //Services
            services.AddTransient<ICatalogService, CatalogService>();

            //Repository
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}
