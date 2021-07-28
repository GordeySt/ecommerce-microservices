using Identity.Application.Common.Interfaces;
using Identity.Grpc.Startup.Settings;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services;
using Identity.Infrastructure.Services.Email;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Grpc.Startup.Configurations
{
    public static class ServicesExtensions
    {
        public static void RegisterServices(this IServiceCollection services,
            AppSettings appSettings)
        {
            //AppSettings
            services.AddSingleton(appSettings);

            //Services
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}
