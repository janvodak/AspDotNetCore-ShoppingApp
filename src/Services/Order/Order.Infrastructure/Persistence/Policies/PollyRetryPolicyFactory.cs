using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Policies
{
	public class PollyyRetryPolicyFactory
	{
		private readonly PollyPolicySettings _pollyPolicySettings;

		public PollyyRetryPolicyFactory(IOptions<PollyPolicySettings> pollyPolicySettings)
		{
			_pollyPolicySettings = pollyPolicySettings.Value;
		}

		public RetryPolicy Create<TContext>(ILogger<TContext> logger) where TContext : DbContext
		{
			return Policy.Handle<SqlException>()
				.WaitAndRetry(
					retryCount: _pollyPolicySettings.MaxRetryAttempts,
					sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(_pollyPolicySettings.SecondsBetweenRetries, retryAttempt)),
					onRetry: (exception, timeSpan, retry, ctx) =>
					{
						logger.LogWarning(
							exception,
							"[{retry} / {retries}] Error occurred during working with [{prefix}]. Exception '{ExceptionType}' with message '{Message}' was detected.",
							retry,
							_pollyPolicySettings.MaxRetryAttempts,
							nameof(TContext),
							exception.GetType().Name,
							exception.Message);
					});
		}
	}
}
