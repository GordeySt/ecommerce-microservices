using Catalog.API.Startup.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Startup.Configuration
{
    public static class AuthenticationExtensions
    {
        public static void RegisterAuthSettings(this IServiceCollection services,
            AppSettings appSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = appSettings.AppUrlsSettings.IdentityUrl;
                    options.ApiName = "catalogapi";
                    options.RequireHttpsMetadata = false;
                    options.RoleClaimType = "role";
                });
        }
    }
}
