using Microsoft.EntityFrameworkCore;

namespace ShoppingApp.Services.Discount.API.Data
{
	public class DiscountContextSeed
	{
		private readonly DiscountContext _discountContext;
		private readonly ILogger<DiscountContextSeed> _logger;

		public DiscountContextSeed(
			DiscountContext discountContext,
			ILogger<DiscountContextSeed> logger)
		{
			_discountContext = discountContext;
			_logger = logger;
		}

		public async Task SeedAsync()
		{
			IEnumerable<string> _pendingMigrations = _discountContext.Database.GetPendingMigrations();

			if (_pendingMigrations.Any() == true)
			{
				await _discountContext.Database.MigrateAsync();

				_logger.LogInformation(
					"Seed database associated with context '{DbContextName}'",
					typeof(DiscountContext).Name);
			}
		}
	}
}
