using Catalog.API.Startup.Settings;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using System;
using System.Net.Http;

namespace Catalog.API.Startup.HttpPolicies
{
    public static class RetryPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(AppSettings appSettings) => 
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: appSettings.RetryPolicySettings.RetryCount,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, retryCount, context) =>
                    {
                        Log.Error($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}");
                    }
                );
    }
}
