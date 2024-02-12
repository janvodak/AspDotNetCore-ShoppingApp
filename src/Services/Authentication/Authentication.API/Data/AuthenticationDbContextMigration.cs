using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly.Retry;

namespace ShoppingApp.Services.Authentication.API.Data
{
	public class AuthenticationDbContextMigration
	{
		private readonly AuthenticationDbContext _applicationDbContext;

		private readonly ILogger<AuthenticationDbContextMigration> _logger;

		private readonly RetryPolicy _retryPolicy;

		public AuthenticationDbContextMigration(
			AuthenticationDbContext applicationContext,
			ILogger<AuthenticationDbContextMigration> logger,
			RetryPolicy retryPolicy)
		{
			_applicationDbContext = applicationContext;
			_logger = logger;
			_retryPolicy = retryPolicy;
		}

		public void Migrate()
		{
			bool _hasSomeMigrationToMigrate = false;

			_logger.LogInformation(
				"Migrating database associated with context '{DbContextName}'.",
				typeof(AuthenticationDbContext).Name);

			try
			{
				_retryPolicy.Execute(() =>
				{
					_hasSomeMigrationToMigrate = _applicationDbContext.Database.GetPendingMigrations().Any();

					if (_hasSomeMigrationToMigrate == true)
					{
						_applicationDbContext.Database.Migrate();
					}
				});
			}
			catch (SqlException ex)
			{
				_logger.LogError(
					ex,
					"An error occurred while migrating the database used on context {DbContextName}.",
					typeof(AuthenticationDbContext).Name);

				return;
			}

			if (_hasSomeMigrationToMigrate == false)
			{
				_logger.LogInformation(
					"No migrations to migrate for DB context associated with '{DbContextName}'.",
					typeof(AuthenticationDbContext).Name);

				return;
			}

			_logger.LogInformation(
				"Database associated with context {DbContextName} was migrated successfully.",
				typeof(AuthenticationDbContext).Name);
		}
	}
}
