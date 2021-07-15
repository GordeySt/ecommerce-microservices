using Catalog.API.DAL;
using Catalog.API.DAL.Interfaces;
using Catalog.API.Startup.Configuration;
using Catalog.API.Startup.Middlewares;
using Catalog.API.Startup.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Catalog.API.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = ReadAppSettings(Configuration);

            services.ValidateSettingParameters(Configuration);
            services.RegisterServices(appSettings);
            services.RegisterAutoMapper();

            services.AddControllers();
            services.RegisterSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerApplication();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static AppSettings ReadAppSettings(IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection(nameof(AppSettings.DbSettings))
                .Get<DbSettings>();
            var cloudinarySettings = configuration.GetSection(nameof(AppSettings.CloudinarySettings))
                .Get<CloudinarySettings>();

            return new AppSettings
            {
                DbSettings = dbSettings,
                CloudinarySettings = cloudinarySettings
            };
        }
    }
}
