using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Startup.Configuration
{
    public static class AuthenticationExtensions
    {
        public static void RegisterAuthSettings(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:5010";
                    options.ApiName = "catalogapi";
                    options.RequireHttpsMetadata = false;
                    options.RoleClaimType = "role";
                });
        }
    }
}
