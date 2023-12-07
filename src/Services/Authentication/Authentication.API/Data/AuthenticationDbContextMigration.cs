using Microsoft.EntityFrameworkCore;

namespace ShoppingApp.Services.Authentication.API.Data
{
	public class AuthenticationDbContextMigration
	{
		private readonly AuthenticationDbContext _applicationDbContext;
		private readonly ILogger<AuthenticationDbContextMigration> _logger;

		public AuthenticationDbContextMigration(
			AuthenticationDbContext applicationContext,
			ILogger<AuthenticationDbContextMigration> logger)
		{
			_applicationDbContext = applicationContext;
			_logger = logger;
		}

		public async Task MigrateAsync()
		{
			IEnumerable<string> _pendingMigrations = _applicationDbContext.Database.GetPendingMigrations();

			if (_pendingMigrations.Any() == true)
			{
				await _applicationDbContext.Database.MigrateAsync();

				_logger.LogInformation(
					"Migrate database associated with context '{DbContextName}'",
					typeof(AuthenticationDbContext).Name);
			}
		}
	}
}
