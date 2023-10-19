using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Src.Contracts.Notifications;
using Order.Application.Src.Contracts.Persistence;
using Order.Application.Src.Models;
using Order.Infrastructure.Src.Notifications;
using Order.Infrastructure.Src.Persistence.Context;
using Order.Infrastructure.Src.Persistence.Repositories;

namespace Order.Infrastructure.Src
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection AddInfrastructureServices(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.Configure<DatabaseSettings>(c => configuration.GetSection("DatabaseSettings"));
			services.AddDbContext<OrderContext>();

			services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
			services.AddScoped<IOrderRepository, OrderRepository>();

			services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
			services.AddTransient<IEmailService, EmailService>();

			return services;
		}
	}
}
