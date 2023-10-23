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
			int retries = 1;

			while (retries <= maxRetries)
			{
				using IServiceScope scope = serviceProvider.CreateScope();
				ILogger<TContext> logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
				TContext context = scope.ServiceProvider.GetRequiredService<TContext>();

				logger.LogInformation(
					"{Atempt}. atempt of migrating database associated with context {DbContextName}.",
					retries,
					typeof(TContext).Name);

				try
				{
					context.Database.Migrate();

					seeder(context, serviceProvider);

					logger.LogInformation(
						"Database associated with context {DbContextName} was migrated successfully.",
						typeof(TContext).Name);

					break;
				}
				catch (Exception ex)
				{
					logger.LogError(
						ex,
						"An error occurred while migrating the database used on context {DbContextName}.",
						typeof(TContext).Name);

					Thread.Sleep(2000);

					retries++;
				}
			}
		}
	}
}
