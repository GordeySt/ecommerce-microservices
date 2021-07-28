using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace Catalog.API.Startup.HttpPolicies
{
    public class CircuitBreakerPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
            HttpPolicyExtensions
               .HandleTransientHttpError()
               .CircuitBreakerAsync(
                   handledEventsAllowedBeforeBreaking: 5,
                   durationOfBreak: TimeSpan.FromSeconds(30)
                );
    }
}
