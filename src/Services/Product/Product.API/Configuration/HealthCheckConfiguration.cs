using ShoppingApp.Services.Product.API.Configuration.DataTransferObjects;

namespace ShoppingApp.Services.Product.API.Configuration
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
				.AddMongoDb(
					mongodbConnectionString: databaseSettings.ConnectionString,
					name: "product-mongodb",
					timeout: TimeSpan.FromSeconds(10),
					tags: new[] { "db", "mongodb" });

			return services;
		}
	}
}
