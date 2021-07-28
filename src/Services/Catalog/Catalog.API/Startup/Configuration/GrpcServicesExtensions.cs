using Catalog.API.PL.GrpcServices;
using Catalog.API.Startup.HttpPolicies;
using Catalog.API.Startup.Settings;
using Common.Logging;
using Identity.Grpc.Protos;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Catalog.API.Startup.Configuration
{
    public static class GrpcServicesExtensions
    {
        public static void RegisterGrpcServices(this IServiceCollection services,
            AppSettings appSettings)
        {
            services.AddGrpcClient<UserProtoService.UserProtoServiceClient>
                (o => o.Address = new Uri(appSettings.AppUrlsSettings.IdentityGrpcUrl))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(RetryPolicy.GetRetryPolicy())
                .AddPolicyHandler(CircuitBreakerPolicy.GetCircuitBreakerPolicy());

            services.AddScoped<UserGrpcService>();
        }
    }
}
