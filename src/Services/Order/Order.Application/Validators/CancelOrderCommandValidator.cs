using FluentValidation;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Commands.CancelOrder;

namespace ShoppingApp.Services.Order.API.Application.Commands.CheckoutOrder
{
	public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
	{
		public CancelOrderCommandValidator(ILogger<CancelOrderCommandValidator> logger)
		{
			RuleFor(p => p.Id)
				.NotNull();

			if (logger.IsEnabled(LogLevel.Trace))
			{
				logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
			}
		}
	}
}
