﻿using Basket.API.Startup.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Basket.API.Startup.Configurations
{
    public static class SwaggerExtensions
    {
        public static void RegisterSwagger(this IServiceCollection services,
            AppSettings appSettings)
        {
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(appSettings.AppUrlsSettings.IdentityUrl + "/connect/token"),
                            AuthorizationUrl = new Uri(appSettings.AppUrlsSettings.IdentityUrl + "/connect/authorize"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "basketapi", "Bakset API" }
                            }
                        }
                    }
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement 
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void UseSwaggerApplication(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1");
                c.OAuthClientId("client_id_basket_swagger");
                c.OAuthClientSecret("client_secret_basket_swagger");
            });
        }
    }
}
