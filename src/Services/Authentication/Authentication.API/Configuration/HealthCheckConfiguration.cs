using ShoppingApp.Services.Authentication.API.Data;

namespace ShoppingApp.Services.Authentication.API.Configuration
{
	public static class HealthCheckConfiguration
	{
		public static IServiceCollection ConfigureHealthChecks(
			this IServiceCollection services)
		{
			services.AddHealthChecks() // try to install depency injection extension
				.AddDbContextCheck<AuthenticationDbContext>(
					name: "authentication-mssql",
					tags: new[] { "ready", "db", "mssql" });

			return services;
		}
	}
}
