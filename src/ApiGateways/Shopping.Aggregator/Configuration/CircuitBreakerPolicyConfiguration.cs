using Polly;
using Polly.Extensions.Http;
using Serilog;
using ShoppingApp.ApiGateway.ShoppingAggregator.Configuration.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Configuration
{
	public static class CircuitBreakerPolicyConfiguration
	{
		public static IAsyncPolicy<HttpResponseMessage> Create(IConfiguration configuration)
		{
			CircuitBreakerPolicySettings circuitBreakerPolicySettings = configuration.GetSection(CircuitBreakerPolicySettings.SECTION_NAME)
				.Get<CircuitBreakerPolicySettings>()
				?? throw new ApplicationException("CircuitBreakerPolicySettings is null. Make sure the configuration is set correctly.");

			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.CircuitBreakerAsync(
					handledEventsAllowedBeforeBreaking: circuitBreakerPolicySettings.MaxFailures,
					durationOfBreak: TimeSpan.FromSeconds(circuitBreakerPolicySettings.DurationOfBreak),
					onBreak: (exception, timespan, context) =>
					{
						Log.Error(
							"Circuit is now in a broken state after '{MaxFailuers}' failed attempts. Breaking for '{TotalSeconds}' seconds. Context: '{PolicyKey}'. Exception: '{Exception}'.",
							circuitBreakerPolicySettings.MaxFailures,
							timespan.TotalSeconds,
							exception,
							context.PolicyKey
							);
					},
					onReset: context =>
					{
						Log.Error(
							"Circuit breaker reset. Closed again. Context: '{PolicyKey}'.",
							context.PolicyKey);
					});
		}
	}
}
