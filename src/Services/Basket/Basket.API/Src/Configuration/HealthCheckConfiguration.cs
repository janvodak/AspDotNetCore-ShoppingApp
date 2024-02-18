namespace Basket.API.Src.Configuration
{
	public static class HealthCheckConfiguration
	{
		public static IServiceCollection ConfigureHealthChecks(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			string databaseSettings = configuration.GetValue<string>("CacheSettings:ConnectionString")
				?? throw new ApplicationException("DatabaseSettings is null. Make sure the configuration is set correctly.");

			services.AddHealthChecks()
				.AddRedis(
					redisConnectionString: databaseSettings,
					name: "basket-redis",
					timeout: TimeSpan.FromSeconds(10),
					tags: new[] { "ready", "db", "redis" });

			return services;
		}
	}
}
