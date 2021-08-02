using Basket.API.Startup.Configurations;
using Basket.API.Startup.Middlewares;
using Basket.API.Startup.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Basket.API.Startup
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

            services.RegisterAuthSettings(appSettings);
            services.ValidateSettingParameters(Configuration);
            services.RegisterRedisCache(appSettings);

            services.AddControllers();
            services.RegisterSwagger(appSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerApplication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static AppSettings ReadAppSettings(IConfiguration configuration)
        { 
            var appUrlsSettings = configuration.GetSection(nameof(AppSettings.AppUrlsSettings))
                .Get<AppUrlsSettings>();

            var redisCacheSettings = configuration.GetSection(nameof(AppSettings.RedisCacheSettings))
                .Get<RedisCacheSettings>();

            return new AppSettings
            {
                AppUrlsSettings = appUrlsSettings,
                RedisCacheSettings = redisCacheSettings
            };
        }
    }
}
