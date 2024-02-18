using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using Serilog;
using ShoppingApp.Services.Authentication.API.Configuration.DataTransferObjects;

namespace ShoppingApp.Services.Authentication.API.Data.Policies
{
	public class PollyRetryPolicyFactory
	{
		private readonly PollyPolicySettings _pollyPolicySettings;

		public PollyRetryPolicyFactory(IOptions<PollyPolicySettings> pollyPolicySettings)
		{
			_pollyPolicySettings = pollyPolicySettings.Value;
		}

		public RetryPolicy Create()
		{
			return Policy.Handle<SqlException>()
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
							nameof(AuthenticationDbContext),
							timeSpan.TotalMilliseconds,
							ctx.PolicyKey,
							exception.GetType().Name,
							exception.Message);
					});
		}
	}
}
