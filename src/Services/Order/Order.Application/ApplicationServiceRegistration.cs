using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Services.Order.API.Application.Behaviors;
using ShoppingApp.Services.Order.API.Application.Commands.CancelOrder;
using ShoppingApp.Services.Order.API.Application.Commands.CheckoutOrder;
using ShoppingApp.Services.Order.API.Application.Commands.CreateOrder;
using ShoppingApp.Services.Order.API.Application.Commands.Shared;
using ShoppingApp.Services.Order.API.Application.Validators;

namespace ShoppingApp.Services.Order.API.Application
{
	public static class ApplicationServiceRegistration
	{
		public static IServiceCollection AddAplicationServices(this IServiceCollection services)
		{
			// Configure mediatR
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

				cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
				cfg.AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>));
				cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
				cfg.AddOpenBehavior(typeof(TransactionBehaviorDecorator<,>));
			});

			// Register the command validators for the validator behavior (validators based on FluentValidation library)
			services.AddSingleton<IValidator<CancelOrderCommand>, CancelOrderCommandValidator>();
			services.AddSingleton<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
			services.AddSingleton<IValidator<IdentifiedCommand<CreateOrderCommand, bool>>, IdentifiedCommandValidator>();

			return services;
		}
	}
}
