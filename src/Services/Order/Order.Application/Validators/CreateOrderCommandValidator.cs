using FluentValidation;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Commands.CreateOrder;

namespace ShoppingApp.Services.Order.API.Application.Commands.CheckoutOrder
{
	public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderCommandValidator(ILogger<CreateOrderCommandValidator> logger)
		{
			RuleFor(p => p.UserName)
				.NotEmpty().WithMessage("{UserName} is required")
				.NotNull()
				.MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

			RuleFor(p => p.EmailAddress)
				.NotEmpty().WithMessage("{EmailAddress} is required.");

			RuleFor(p => p.TotalPrice)
				.NotEmpty().WithMessage("{TotalPrice} is required.")
				.GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");

			if (logger.IsEnabled(LogLevel.Trace))
			{
				logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
			}
		}
	}
}
