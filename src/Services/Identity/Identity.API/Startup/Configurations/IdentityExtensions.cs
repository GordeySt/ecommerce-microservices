using Identity.API.Startup.Settings;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Startup.Configurations
{
    public static class IdentityExtensions
    {
        public static void RegisterIdentity(this IServiceCollection services,
            AppSettings appSettings)
        {
            services
                .AddIdentityCore<ApplicationUser>(options =>
                {
                    options.User.RequireUniqueEmail = appSettings.IdentitySettings.Email.RequiredUniqueEmail;
                    options.Password.RequireDigit = appSettings.IdentitySettings.Password.RequireDigit;
                    options.Password.RequireLowercase = appSettings.IdentitySettings.Password.RequireLowercase;
                    options.Password.RequireUppercase = appSettings.IdentitySettings.Password.RequireUppercase;
                    options.Password.RequireNonAlphanumeric = appSettings.IdentitySettings.Password.RequireNonAlphanumeric;
                    options.Password.RequiredUniqueChars = appSettings.IdentitySettings.Password.RequiredUniqueChars;
                })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();
        }
    }
}
