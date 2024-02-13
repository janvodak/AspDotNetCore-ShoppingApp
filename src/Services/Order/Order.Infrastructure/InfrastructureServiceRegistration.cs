using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShoppingApp.Services.Order.API.Application.Contracts.Idempotency;
using ShoppingApp.Services.Order.API.Application.Contracts.Notifications;
using ShoppingApp.Services.Order.API.Application.Contracts.Persistence;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Repositories;
using ShoppingApp.Services.Order.API.Infrastructure.Idempotency.Services;
using ShoppingApp.Services.Order.API.Infrastructure.Notifications;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Behaviors;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Context;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Policies;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Repositories;

namespace ShoppingApp.Services.Order.API.Infrastructure
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection AddInfrastructureServices(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			ConfigureIdempotency(services);
			ConfigurePersistence(services, configuration);
			ConfigureNotifications(services, configuration);

			return services;
		}

		private static void ConfigureIdempotency(IServiceCollection services)
		{
			services.AddScoped<IRequestManager, RequestManager>();
		}

		private static void ConfigurePersistence(
			IServiceCollection services,
			IConfiguration configuration)
		{
			// Database configuration
			IConfigurationSection databaseConfigurationSection = configuration.GetSection(DatabaseSettings.SECTION_NAME);

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

			// Entity Framework Policies configuration
			IConfigurationSection entityFrameworkPolicyConfigurationSection = configuration.GetSection(EntityFrameworkPolicySettings.SECTION_NAME);

			services.Configure<EntityFrameworkPolicySettings>(options =>
			{
				string maxRetryCountValue = entityFrameworkPolicyConfigurationSection[nameof(EntityFrameworkPolicySettings.MaxRetryCount)]
					?? throw new ArgumentNullException(
						nameof(options.MaxRetryCount),
						"value is missing in appsettings.json");
				options.MaxRetryCount = int.Parse(maxRetryCountValue);

				string MaxRetryDelay = entityFrameworkPolicyConfigurationSection[nameof(EntityFrameworkPolicySettings.MaxRetryDelay)]
						?? throw new ArgumentNullException(
							nameof(options.MaxRetryDelay),
							"value is missing in appsettings.json");
				options.MaxRetryDelay = int.Parse(MaxRetryDelay);
			});

			services.AddSingleton(c => c.GetRequiredService<IOptions<EntityFrameworkPolicySettings>>().Value);

			// Polly Policies configuration
			IConfigurationSection pollyPolicyConfigurationSection = configuration.GetSection(PollyPolicySettings.SECTION_NAME);

			services.Configure<PollyPolicySettings>(options =>
			{
				string maxRetryAttemptsValue = pollyPolicyConfigurationSection[nameof(PollyPolicySettings.MaxRetryAttempts)]
					?? throw new ArgumentNullException(
						nameof(options.MaxRetryAttempts),
						"value is missing in appsettings.json");
				options.MaxRetryAttempts = int.Parse(maxRetryAttemptsValue);

				string secondsBetweenRetriesValue = pollyPolicyConfigurationSection[nameof(PollyPolicySettings.SecondsBetweenRetries)]
					?? throw new ArgumentNullException(
						nameof(options.SecondsBetweenRetries),
						"value is missing in appsettings.json");
				options.SecondsBetweenRetries = int.Parse(secondsBetweenRetriesValue);
			});

			services.AddSingleton(c => c.GetRequiredService<IOptions<PollyPolicySettings>>().Value);

			services.AddDbContext<OrderContext>();

			// Register your interfaces and implementations here
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped(typeof(ITransactionBehavior<,>), typeof(TransactionBehavior<,>));

			services.AddScoped<PollyyRetryPolicyFactory>();
		}

		private static void ConfigureNotifications(
			IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddScoped<IEmailService, EmailService>();

			IConfigurationSection emailConfigurationSection = configuration.GetSection(EmailSettings.SECTION_NAME);

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
