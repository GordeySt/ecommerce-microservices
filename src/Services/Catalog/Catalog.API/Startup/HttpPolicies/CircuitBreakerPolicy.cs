using Catalog.API.Startup.Settings;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace Catalog.API.Startup.HttpPolicies
{
    public class CircuitBreakerPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(AppSettings appSettings) =>
            HttpPolicyExtensions
               .HandleTransientHttpError()
               .CircuitBreakerAsync(
                   handledEventsAllowedBeforeBreaking: appSettings.CircuitBreakerSettings.HandledEventsAllowedBeforeBreaking,
                   durationOfBreak: TimeSpan.FromSeconds(appSettings.CircuitBreakerSettings.DurationOfBreak)
                );
    }
}
