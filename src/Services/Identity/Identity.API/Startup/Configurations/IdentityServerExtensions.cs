using Identity.API.Configurations;
using Identity.API.Services;
using Identity.API.Startup.Settings;
using Identity.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Startup.Configurations
{
    public static class IdentityServerExtensions
    {
        public static void RegisterIdentityServer(this IServiceCollection services,
            AppSettings appSettings)
        {
            services.AddIdentityServer()
                .AddAspNetIdentity<ApplicationUser>()
                .AddInMemoryApiResources(IdentityConfiguration.Apis)
                .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
                .AddInMemoryApiScopes(IdentityConfiguration.Scopes)
                .AddInMemoryClients(IdentityConfiguration.GetClients(appSettings))
                .AddDeveloperSigningCredential()
                .AddProfileService<ProfileService>();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/AuthMvc/Login";
                config.LogoutPath = "/AuthMvc/Logout";
            });
        }
    }
}
