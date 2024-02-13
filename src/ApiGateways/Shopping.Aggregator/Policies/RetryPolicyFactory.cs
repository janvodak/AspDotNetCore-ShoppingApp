using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Serilog;
using ShoppingApp.ApiGateway.ShoppingAggregator.Settings;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Policies
{
	public class RetryPolicyFactory
	{
		private readonly RetryPolicySettings _retryPolicySettings;

		public RetryPolicyFactory(IOptions<RetryPolicySettings> retryPolicySettings)
		{
			_retryPolicySettings = retryPolicySettings.Value;
		}

		public AsyncRetryPolicy<HttpResponseMessage> Create<T>() where T : class
		{
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
					retryCount: _retryPolicySettings.MaxRetryAttempts,
					sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(_retryPolicySettings.SecondsBetweenRetries, retryAttempt)) + TimeSpan.FromMilliseconds(jitterer.Next(0, _retryPolicySettings.JittererLimit)),
					onRetry: (result, timeSpan, retry, ctx) =>
					{
						Log.Error(
							"[{retry} / {retries}] Error occurred during work with '[{prefix}]'. Action was retried after '{Timespan}' miliseconds. Context '{PolicyKey}'. Message '{Message}' was detected.",
							retry,
							_retryPolicySettings.MaxRetryAttempts,
							className,
							timeSpan.TotalMilliseconds,
							ctx.PolicyKey,
							result);
					});
		}
	}
}
