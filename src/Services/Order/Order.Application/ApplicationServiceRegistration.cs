using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Services.Order.API.Application.Behaviours;
using ShoppingApp.Services.Order.API.Application.Services;

namespace ShoppingApp.Services.Order.API.Application
{
	public static class ApplicationServiceRegistration
	{
		public static IServiceCollection AddAplicationServices(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddMediatR(Assembly.GetExecutingAssembly());

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

			services.AddScoped<CheckoutOrderEmailService>();

			return services;
		}
	}
}
