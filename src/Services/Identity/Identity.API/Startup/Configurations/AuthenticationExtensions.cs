using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Identity.API.Startup.Configurations
{
    public static class AuthenticationExtensions
    {
        public static void RegisterAuthSettings(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["appUrls:identityUrl"];
                    options.ApiName = "identityapi";
                    options.RequireHttpsMetadata = false;
                    options.RoleClaimType = "role";
                });
        }
    }
}
