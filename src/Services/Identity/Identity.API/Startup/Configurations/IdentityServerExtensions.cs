using Identity.API.Configurations;
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
                .AddInMemoryApiScopes(IdentityConfiguration.Scopes)
                .AddInMemoryClients(IdentityConfiguration.GetClients(appSettings))
                .AddDeveloperSigningCredential();
        }
    }
}
