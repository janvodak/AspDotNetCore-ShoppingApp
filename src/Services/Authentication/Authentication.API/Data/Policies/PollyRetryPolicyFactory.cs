using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using Serilog;

namespace ShoppingApp.Services.Authentication.API.Data.Policies
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
			return Policy.Handle<SqlException>()
				.WaitAndRetry(
					retryCount: _pollyPolicySettings.MaxRetryAttempts,
					sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(_pollyPolicySettings.SecondsBetweenRetries, retryAttempt)),
					onRetry: (exception, timeSpan, retry, ctx) =>
					{
						Log.Warning(
							exception,
							"[{retry} / {retries}] Error occurred during working with [{prefix}]. Exception '{ExceptionType}' with message '{Message}' was detected.",
							retry,
							_pollyPolicySettings.MaxRetryAttempts,
							nameof(AuthenticationDbContext),
							exception.GetType().Name,
							exception.Message);
					});
		}
	}
}
