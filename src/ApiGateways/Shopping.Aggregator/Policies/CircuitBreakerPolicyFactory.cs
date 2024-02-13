using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using ShoppingApp.ApiGateway.ShoppingAggregator.Settings;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Policies
{
	public class CircuitBreakerPolicyFactory
	{
		private readonly CircuitBreakerPolicySettings _circuitBreakerPolicySettings;

		public CircuitBreakerPolicyFactory(IOptions<CircuitBreakerPolicySettings> circuitBreakerPolicySettings)
		{
			_circuitBreakerPolicySettings = circuitBreakerPolicySettings.Value;
		}

		public IAsyncPolicy<HttpResponseMessage> Create()
		{
			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.CircuitBreakerAsync(
					handledEventsAllowedBeforeBreaking: _circuitBreakerPolicySettings.MaxFailures,
					durationOfBreak: TimeSpan.FromSeconds(_circuitBreakerPolicySettings.DurationOfBreak),
					onBreak: (exception, timespan, context) =>
					{
						Log.Error(
							"Circuit is now in a broken state after '{MaxFailuers}' failed attempts. Breaking for '{TotalSeconds}' seconds. Context: '{PolicyKey}'. Exception: '{Exception}'.",
							_circuitBreakerPolicySettings.MaxFailures,
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
