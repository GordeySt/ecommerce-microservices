using Catalog.API.Startup.Configuration;
using Catalog.API.Startup.Middlewares;
using Catalog.API.Startup.Settings;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Catalog.API.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            var appSettings = ReadAppSettings(Configuration, Env);

            services.RegisterDatabase(appSettings);
            services.RegisterAuthSettings(appSettings);
            services.ValidateSettingParameters(Configuration);
            services.RegisterAutoMapper();
            services.RegisterGrpcServices(appSettings);
            services.RegisterRedisCache(appSettings);

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddNewtonsoftJson(options => 
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.RegisterServices(appSettings);

            services.RegisterSwagger(appSettings);
            services.RegisterHealthChecks(appSettings);
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

            app.UseCors(x => x
                .SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }

        private static AppSettings ReadAppSettings(IConfiguration configuration,
            IWebHostEnvironment env)
        {
            var dbSettings = configuration.GetSection(nameof(AppSettings.DbSettings))
                .Get<DbSettings>();

            var cloudinarySettings = configuration.GetSection(nameof(AppSettings.CloudinarySettings))
                .Get<CloudinarySettings>();

            var appUrlsSettings = configuration.GetSection(nameof(AppSettings.AppUrlsSettings))
                .Get<AppUrlsSettings>();

            var retryPolicySettings = configuration.GetSection(nameof(AppSettings.RetryPolicySettings))
                .Get<RetryPolicySettings>();

            var circuitBreakerSettings = configuration.GetSection(nameof(AppSettings.CircuitBreakerSettings))
                .Get<CircuitBreakerSettings>();

            var redisCacheSettings = configuration.GetSection(nameof(AppSettings.RedisCacheSettings))
                .Get<RedisCacheSettings>();

            if (env.IsDevelopment())
            {
                cloudinarySettings.ApiSecret = configuration["Cloudinary:ApiSecret"];
            }

            return new AppSettings
            {
                DbSettings = dbSettings,
                CloudinarySettings = cloudinarySettings,
                AppUrlsSettings = appUrlsSettings,
                RetryPolicySettings = retryPolicySettings,
                CircuitBreakerSettings = circuitBreakerSettings,
                RedisCacheSettings = redisCacheSettings
            };
        }
    }
}
