using Microsoft.Extensions.Options;
using Npgsql;
using Polly;
using Polly.Retry;
using Serilog;

namespace ShoppingApp.Services.Discount.API.Data.Policies
{
	public class PollyyRetryPolicyFactory
	{
		private readonly PollyPolicySettings _pollyPolicySettings;

		public PollyyRetryPolicyFactory(IOptions<PollyPolicySettings> pollyPolicySettings)
		{
			_pollyPolicySettings = pollyPolicySettings.Value;
		}

		public RetryPolicy Create()
		{
			return Policy.Handle<NpgsqlException>()
				.WaitAndRetry(
					retryCount: _pollyPolicySettings.MaxRetryAttempts,
					sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(_pollyPolicySettings.SecondsBetweenRetries, retryAttempt)),
					onRetry: (exception, timeSpan, retry, ctx) =>
					{
						Log.Warning(
							exception,
							"[{retry} / {retries}] Error occurred during work with '[{prefix}]'. Action was retried after '{Timespan}' miliseconds. Context '{PolicyKey}'. Exception '{ExceptionType}' with message '{Message}' was detected.",
							retry,
							_pollyPolicySettings.MaxRetryAttempts,
							nameof(DiscountDbContext),
							timeSpan.TotalMilliseconds,
							ctx.PolicyKey,
							exception.GetType().Name,
							exception.Message);
					});
		}
	}
}
