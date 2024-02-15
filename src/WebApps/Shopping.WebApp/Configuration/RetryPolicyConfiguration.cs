using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Serilog;
using Shopping.WebApp.Configuration.DataTransferObjects;

namespace Shopping.WebApp.Configuration
{
	public static class RetryPolicyConfiguration
	{
		public static AsyncRetryPolicy<HttpResponseMessage> Create<T>(
			IConfiguration configuration) where T : class
		{
			RetryPolicySettings retryPolicySettings = configuration.GetSection(RetryPolicySettings.SECTION_NAME)
				.Get<RetryPolicySettings>()
				?? throw new ApplicationException("CircuitBreakerPolicySettings is null. Make sure the configuration is set correctly.");

			// exponential back-off: 2, 4, 8 etc
			//  2 ^ 1 = 2 seconds then
			//  2 ^ 2 = 4 seconds then
			//  2 ^ 3 = 8 seconds then
			//  2 ^ 4 = 16 seconds then
			//  2 ^ 5 = 32 seconds
			// plus some jitter: up to 1 second
			Random jitterer = new();
			string className = typeof(T).Name;

			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
				.WaitAndRetryAsync(
					retryCount: retryPolicySettings.MaxRetryAttempts,
					sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(retryPolicySettings.SecondsBetweenRetries, retryAttempt)) + TimeSpan.FromMilliseconds(jitterer.Next(0, retryPolicySettings.JittererLimit)),
					onRetry: (result, timeSpan, retry, ctx) =>
					{
						Log.Error(
							"[{retry} / {retries}] Error occurred during work with '[{prefix}]'. Action was retried after '{Timespan}' miliseconds. Context '{PolicyKey}'. Message '{Message}' was detected.",
							retry,
							retryPolicySettings.MaxRetryAttempts,
							className,
							timeSpan.TotalMilliseconds,
							ctx.PolicyKey,
							result);
					});
		}
	}
}
