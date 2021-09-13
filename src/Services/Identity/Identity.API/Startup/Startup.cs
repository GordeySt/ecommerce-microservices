using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Identity.API.Configurations;
using Identity.API.Startup.Configurations;
using Identity.API.Startup.Middlewares;
using Identity.API.Startup.Settings;
using Identity.Application.ApplicationUsers.Commands.SignupUsers;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services.Email;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Identity.API.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = ReadAppSettings(Configuration);

            services.ValidateSettingParameters(Configuration);
            services.RegisterDatabase(appSettings);
            services.RegisterServices(appSettings);
            services.RegisterAuthSettings(appSettings);
            services.RegisterIdentity(appSettings);
            services.RegisterIdentityServer(appSettings);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins(appSettings.AppUrlsSettings.ClientUrl)
                        .AllowCredentials();
                });
            });

            services.RegisterAutoMapper();
            services.RegisterMediatr();

            services.Configure<SmtpClientSettings>(Configuration.GetSection(nameof(SmtpClientSettings)));

            services.RegisterSwagger(appSettings);
            services.RegisterHealthChecks(appSettings);

            services.AddControllersWithViews()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<SignupUserCommand>();
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerApplication();

            app.UseCors("CorsPolicy");

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "Styles")),
                RequestPath = "/styles"
            });

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }

        private static AppSettings ReadAppSettings(IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection(nameof(AppSettings.DbSettings))
                .Get<DbSettings>();

            var identitySettings = configuration.GetSection(nameof(AppSettings.IdentitySettings))
                .Get<IdentitySettings>();

            var appUrlsSettings = configuration.GetSection(nameof(AppSettings.AppUrlsSettings))
                .Get<AppUrlsSettings>();

            return new AppSettings
            {
                DbSettings = dbSettings,
                IdentitySettings = identitySettings,
                AppUrlsSettings = appUrlsSettings
            };
        }
    }
}
