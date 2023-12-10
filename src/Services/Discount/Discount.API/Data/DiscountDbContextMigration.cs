using Microsoft.EntityFrameworkCore;

namespace ShoppingApp.Services.Discount.API.Data
{
	public class DiscountDbContextMigration
	{
		private readonly DiscountDbContext _discountDbContext;
		private readonly ILogger<DiscountDbContextMigration> _logger;

		public DiscountDbContextMigration(
			DiscountDbContext discountDbContext,
			ILogger<DiscountDbContextMigration> logger)
		{
			_discountDbContext = discountDbContext;
			_logger = logger;
		}

		public async Task MigrateAsync()
		{
			IEnumerable<string> _pendingMigrations = _discountDbContext.Database.GetPendingMigrations();

			if (_pendingMigrations.Any() == true)
			{
				await _discountDbContext.Database.MigrateAsync();

				_logger.LogInformation(
					"Seed database associated with context '{DbContextName}'",
					typeof(DiscountDbContext).Name);
			}
		}
	}
}
