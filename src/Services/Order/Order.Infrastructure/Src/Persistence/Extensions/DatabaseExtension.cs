using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Order.Infrastructure.Src.Persistence.Extensions
{
	public static class DatabaseExtensions
	{
		public static void MigrateDatabase<TContext>(
			this IServiceProvider serviceProvider,
			Action<TContext, IServiceProvider> seeder,
			int maxRetries = 50) where TContext : DbContext
		{
			int retries = 0;

			while (retries < maxRetries)
			{
				using IServiceScope scope = serviceProvider.CreateScope();
				ILogger<TContext> logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
				TContext context = scope.ServiceProvider.GetRequiredService<TContext>();

				try
				{
					logger.LogInformation(
						"Migrating database associated with context {DbContextName}",
						typeof(TContext).Name);

					context.Database.Migrate();

					seeder(context, serviceProvider);

					logger.LogInformation(
						"Migrated database associated with context {DbContextName}",
						typeof(TContext).Name);

					break;
				}
				catch (Exception ex)
				{
					logger.LogError(
						ex,
						"An error occurred while migrating the database used on context {DbContextName}",
						typeof(TContext).Name);

					if (retries < maxRetries - 1)
					{
						// Implement a delay before retrying
						Thread.Sleep(2000);
					}

					retries++;
				}
			}
		}
	}
}
