using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using Serilog;
using ShoppingApp.Services.Authentication.API.Data;

namespace ShoppingApp.Services.Authentication.API.Settings
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
					onRetry: (exception, retryCount, context) =>
					{
						Log.Error(
							exception,
							"An error occurred while migrating the database associated with context '{DbContextName}'. Time after retry '{RetryCount}' of policy key '{PolicyKey}' at operation key '{OperationKey}'.",
							typeof(AuthenticationDbContext).Name,
							retryCount,
							context.PolicyKey,
							context.OperationKey);
					});
		}
	}
}
