using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Order.Application.Src.Contracts.Notifications;
using Order.Application.Src.Contracts.Persistence;
using Order.Infrastructure.Src.Notifications;
using Order.Infrastructure.Src.Persistence.Context;
using Order.Infrastructure.Src.Persistence.Repositories;

namespace Order.Infrastructure.Src
{
	public static class InfrastructureServiceRegistration
	{
		private const string DATABASE_SETTINGS_SECTION_NAME = "DatabaseSettings";
		private const string EMAIL_SETTINGS_SECTION_NAME = "EmailSettings";

		public static IServiceCollection AddInfrastructureServices(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			ConfigureDatabaseSettings(services, configuration);
			ConfigureDatabaseContext(services);
			ConfigureEmailSettings(services, configuration);

			return services;
		}

		private static void ConfigureDatabaseSettings(
			IServiceCollection services,
			IConfiguration configuration)
		{
			IConfigurationSection databaseConfigurationSection = configuration.GetSection(InfrastructureServiceRegistration.DATABASE_SETTINGS_SECTION_NAME);

			services.Configure<DatabaseSettings>(options =>
			{
				options.ConnectionStringTemplate = databaseConfigurationSection[nameof(DatabaseSettings.ConnectionStringTemplate)]
					?? throw new ArgumentNullException(
						nameof(options.ConnectionStringTemplate),
						"value is missing in appsettings.json");
				options.Server = databaseConfigurationSection[nameof(DatabaseSettings.Server)]
					?? throw new ArgumentNullException(
						nameof(options.Server),
						"value is missing in appsettings.json");
				options.DBname = databaseConfigurationSection[nameof(DatabaseSettings.DBname)]
					?? throw new ArgumentNullException(
						nameof(options.DBname),
						"value is missing in appsettings.json");
				options.User = databaseConfigurationSection[nameof(DatabaseSettings.User)]
					?? throw new ArgumentNullException(
						nameof(options.User),
						"value is missing in appsettings.json");
				options.Password = databaseConfigurationSection[nameof(DatabaseSettings.Password)]
					?? throw new ArgumentNullException(
						nameof(options.Password),
						"value is missing in appsettings.json");
			});

			services.AddSingleton(c => c.GetRequiredService<IOptions<DatabaseSettings>>().Value);
		}

		private static void ConfigureDatabaseContext(IServiceCollection services)
		{
			services.AddDbContext<OrderContext>();
			services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
			services.AddScoped<IOrderRepository, OrderRepository>();
		}

		private static void ConfigureEmailSettings(
			IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddTransient<IEmailService, EmailService>();

			IConfigurationSection emailConfigurationSection = configuration.GetSection(InfrastructureServiceRegistration.EMAIL_SETTINGS_SECTION_NAME);

			services.Configure<EmailSettings>(options =>
			{
				options.ApiKey = emailConfigurationSection[nameof(EmailSettings.ApiKey)]
					?? throw new ArgumentNullException(
						nameof(options.ApiKey),
						"value is missing in appsettings.json");
				options.FromAddress = emailConfigurationSection[nameof(EmailSettings.FromAddress)]
					?? throw new ArgumentNullException(
						nameof(options.FromAddress),
						"value is missing in appsettings.json");
				options.FromName = emailConfigurationSection[nameof(EmailSettings.FromName)]
					?? throw new ArgumentNullException(
						nameof(options.FromName),
						"value is missing in appsettings.json");
			});

			services.AddSingleton(c => c.GetRequiredService<IOptions<EmailSettings>>().Value);
		}
	}
}
