using Microsoft.EntityFrameworkCore;
using Npgsql;
using Polly.Retry;

namespace ShoppingApp.Services.Discount.API.Data
{
	public class DiscountDbContextMigration
	{
		private readonly DiscountDbContext _discountDbContext;

		private readonly ILogger<DiscountDbContextMigration> _logger;

		private readonly RetryPolicy _retryPolicy;

		public DiscountDbContextMigration(
			DiscountDbContext discountDbContext,
			ILogger<DiscountDbContextMigration> logger,
			RetryPolicy retryPolicy)
		{
			_discountDbContext = discountDbContext;
			_logger = logger;
			_retryPolicy = retryPolicy;
		}

		public void Migrate()
		{
			int _numberOfMigrations = 0;

			_logger.LogInformation(
				"Migrating database associated with context '{DbContextName}'.",
				typeof(DiscountDbContextMigration).Name);

			try
			{
				_retryPolicy.Execute(() =>
				{
					_numberOfMigrations = _discountDbContext.Database.GetPendingMigrations().Count();

					if (_numberOfMigrations > 0)
					{
						_discountDbContext.Database.Migrate();
					}
				});
			}
			catch (NpgsqlException ex)
			{
				_logger.LogError(
					ex,
					"An error occurred while migrating the database used on context {DbContextName}.",
					nameof(DiscountDbContextMigration));
			}

			_logger.LogInformation(
				"Database associated with context {DbContextName} was migrated successfully with '{NumberOfMigrations}' new migrations.",
				nameof(DiscountDbContext),
				_numberOfMigrations);
		}
	}
}
