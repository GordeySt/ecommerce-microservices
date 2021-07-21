using Identity.API.Configurations;
using Identity.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Startup.Configurations
{
    public static class IdentityServerExtensions
    {
        public static void RegisterIdentityServer(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddIdentityServer()
                .AddAspNetIdentity<ApplicationUser>()
                .AddInMemoryApiResources(IdentityConfiguration.Apis)
                .AddInMemoryApiScopes(IdentityConfiguration.Scopes)
                .AddInMemoryClients(IdentityConfiguration.GetClients(configuration))
                .AddDeveloperSigningCredential();
        }
    }
}
