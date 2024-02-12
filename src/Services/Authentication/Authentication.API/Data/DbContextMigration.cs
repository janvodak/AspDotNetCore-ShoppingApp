using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly.Retry;

namespace ShoppingApp.Services.Authentication.API.Data
{
	public class DbContextMigration
	{
		private readonly AuthenticationDbContext _authenticationDbContext;

		private readonly ILogger<DbContextMigration> _logger;

		private readonly RetryPolicy _retryPolicy;

		public DbContextMigration(
			AuthenticationDbContext authenticationDbContext,
			ILogger<DbContextMigration> logger,
			RetryPolicy retryPolicy)
		{
			_authenticationDbContext = authenticationDbContext;
			_logger = logger;
			_retryPolicy = retryPolicy;
		}

		public void Migrate()
		{
			int _numberOfMigrations = 0;

			_logger.LogInformation(
				"Migrating database associated with context '{DbContextName}'.",
				typeof(AuthenticationDbContext).Name);

			try
			{
				_retryPolicy.Execute(() =>
				{
					_numberOfMigrations = _authenticationDbContext.Database.GetPendingMigrations().Count();

					if (_numberOfMigrations > 0)
					{
						_authenticationDbContext.Database.Migrate();
					}
				});
			}
			catch (SqlException ex)
			{
				_logger.LogError(
					ex,
					"An error occurred while migrating the database used on context {DbContextName}.",
					nameof(AuthenticationDbContext));
			}

			_logger.LogInformation(
				"Database associated with context {DbContextName} was migrated successfully with '{NumberOfMigrations}' new migrations.",
				nameof(AuthenticationDbContext),
				_numberOfMigrations);
		}
	}
}
