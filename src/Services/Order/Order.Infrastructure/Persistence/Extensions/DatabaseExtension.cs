using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly.Retry;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Policies;

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Extensions
{
	public static class DatabaseExtensions
	{
		public static void MigrateDatabase<TContext>(
			this IServiceProvider serviceProvider,
			Action<TContext, IServiceProvider> seeder) where TContext : DbContext
		{
			using (IServiceScope scope = serviceProvider.CreateScope())
			{
				ILogger<TContext> logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
				TContext context = scope.ServiceProvider.GetRequiredService<TContext>();

				PollyyRetryPolicyFactory pollyyRetryPolicyFactory = scope.ServiceProvider.GetRequiredService<PollyyRetryPolicyFactory>();
				RetryPolicy retryPolicy = pollyyRetryPolicyFactory.Create<TContext>(logger);

				int _numberOfMigrations = 0;

				logger.LogInformation(
					"Migrating database associated with context '{DbContextName}'.",
					nameof(TContext));

				try
				{                
					retryPolicy.Execute(() =>
					{
						_numberOfMigrations = context.Database.GetPendingMigrations().Count();

						if (_numberOfMigrations > 0)
						{
							context.Database.Migrate();
						}

						seeder(context, serviceProvider);
					});
				}
				catch (SqlException ex)
				{
					logger.LogError(
						ex,
						"An error occurred while migrating the database used on context {DbContextName}.",
						nameof(TContext));
				}

				logger.LogInformation(
					"Database associated with context {DbContextName} was migrated successfully with '{NumberOfMigrations}' new migrations.",
					nameof(TContext),
					_numberOfMigrations);
			}
		}
	}
}
