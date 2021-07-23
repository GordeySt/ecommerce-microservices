using Catalog.API.PL.GrpcServices;
using Catalog.API.Startup.Configuration;
using Catalog.API.Startup.Middlewares;
using Catalog.API.Startup.Settings;
using HealthChecks.UI.Client;
using Identity.Grpc.Protos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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
            var appSettings = ReadAppSettings(Configuration, Env);

            services.RegisterAuthSettings(Configuration);
            services.ValidateSettingParameters(Configuration);
            services.RegisterServices(appSettings);
            services.RegisterAutoMapper();

            services.AddGrpcClient<UserProtoService.UserProtoServiceClient>
                (o => o.Address = new Uri("http://localhost:5001"));

            services.AddScoped<UserGrpcService>();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.RegisterSwagger(Configuration);
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

            if (env.IsDevelopment())
            {
                cloudinarySettings.ApiSecret = configuration["Cloudinary:ApiSecret"];
            }

            return new AppSettings
            {
                DbSettings = dbSettings,
                CloudinarySettings = cloudinarySettings
            };
        }
    }
}
