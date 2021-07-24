using Catalog.API.PL.GrpcServices;
using Catalog.API.Startup.HttpPolicies;
using Common.Logging;
using Identity.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Catalog.API.Startup.Configuration
{
    public static class GrpcServicesExtensions
    {
        public static void RegisterGrpcServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddGrpcClient<UserProtoService.UserProtoServiceClient>
                (o => o.Address = new Uri(configuration["appUrls:identityGrpcUrl"]))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(RetryPolicy.GetRetryPolicy())
                .AddPolicyHandler(CircuitBreakerPolicy.GetCircuitBreakerPolicy());

            services.AddScoped<UserGrpcService>();
        }
    }
}
