using Shopping.WebApp.Configuration.DataTransferObjects;

namespace Shopping.WebApp.Configuration
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
					uri: new Uri(databaseSettings.OcelotApiGatewayUrl),
					name: "ocelot-api-gateway",
					timeout: TimeSpan.FromSeconds(10),
					tags: new[] { "ready", "client" });

			return services;
		}
	}
}
