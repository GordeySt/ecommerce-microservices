using FluentValidation.AspNetCore;
using Identity.API.Startup.Configurations;
using Identity.API.Startup.Middlewares;
using Identity.API.Startup.Settings;
using Identity.Application.ApplicationUsers.Commands.SignupUsers;
using Identity.Infrastructure.Services.Email;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddControllers()
                 .AddFluentValidation(config =>
                 {
                     config.RegisterValidatorsFromAssemblyContaining<SignupUserCommand>();
                 });

            services.RegisterDatabase(appSettings);
            services.RegisterAuthSettings(Configuration);
            services.RegisterIdentity(appSettings);
            services.RegisterIdentityServer(Configuration);
            services.ValidateSettingParameters(Configuration);

            services.RegisterServices(appSettings);

            services.RegisterAutoMapper();
            services.RegisterMediatr();

            services.Configure<SmtpClientSettings>(Configuration.GetSection(nameof(SmtpClientSettings)));

            services.RegisterSwagger(Configuration);
        }

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

            app.UseIdentityServer();

            app.UseAuthentication();
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

            var identitySettings = configuration.GetSection(nameof(AppSettings.IdentitySettings))
                .Get<IdentitySettings>();

            return new AppSettings
            {
                DbSettings = dbSettings,
                IdentitySettings = identitySettings
            };
        }
    }
}
