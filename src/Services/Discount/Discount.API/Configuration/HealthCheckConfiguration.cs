using ShoppingApp.Services.Discount.API.Configuration.DataTransferObjects;

namespace ShoppingApp.Services.Discount.API.Configuration
{
	public static class HealthCheckConfiguration
	{
		public static IServiceCollection ConfigureHealthChecks(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			DatabaseSettings databaseSettings = configuration.GetSection(DatabaseSettings.SECTION_NAME)
				.Get<DatabaseSettings>()
				?? throw new ApplicationException("DatabaseSettings is null. Make sure the configuration is set correctly.");

			services.AddHealthChecks()
				.AddNpgSql(
					connectionString: databaseSettings.GetConnectionString(),
					name: "discount-postgres",
					timeout: TimeSpan.FromSeconds(10),
					tags: new[] { "ready", "db", "postgres" });

			return services;
		}
	}
}
