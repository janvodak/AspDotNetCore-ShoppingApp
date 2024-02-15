using ShoppingApp.ApiGateway.ShoppingAggregator.Configuration.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Configuration
{
	public static class HealthCheckConfiguration
	{
		public static IServiceCollection ConfigureHealthChecks(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			ApiServicesSettings databaseSettings = configuration.GetSection(ApiServicesSettings.SECTION_NAME)
				.Get<ApiServicesSettings>()
				?? throw new ApplicationException("ApiServicesSettings is null. Make sure the configuration is set correctly.");

			services.AddHealthChecks()
				.AddUrlGroup(
					uri: new Uri($"{databaseSettings.BasketApiUrl}/health/ready"),
					name: "basket-api",
					timeout: TimeSpan.FromSeconds(10),
					tags: new[] { "ready", "client" })
				.AddUrlGroup(
					uri: new Uri($"{databaseSettings.OrderApiUrl}/health/ready"),
					name: "order-api",
					timeout: TimeSpan.FromSeconds(10),
					tags: new[] { "ready", "client" })
				.AddUrlGroup(
					uri: new Uri($"{databaseSettings.ProductApiUrl}/health/ready"),
					name: "product-api",
					timeout: TimeSpan.FromSeconds(10),
					tags: new[] { "ready", "client" });

			return services;
		}
	}
}
