using Identity.API.Services;
using Identity.API.Startup.Settings;
using Identity.Application.Common.Interfaces;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services;
using Identity.Infrastructure.Services.Email;
using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Startup.Configurations
{
    public static class ServicesExtensions
    {
        public static void RegisterServices(this IServiceCollection services,
            AppSettings appSettings)
        {
            //AppSettings
            services.AddSingleton(appSettings);

            //Services
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}
