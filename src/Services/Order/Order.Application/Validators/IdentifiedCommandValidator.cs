using FluentValidation;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Commands.CreateOrder;
using ShoppingApp.Services.Order.API.Application.Commands.Shared;

namespace ShoppingApp.Services.Order.API.Application.Validators
{
	public class IdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<CreateOrderCommand, bool>>
	{
		public IdentifiedCommandValidator(ILogger<IdentifiedCommandValidator> logger)
		{
			RuleFor(command => command.Id)
				.NotEmpty().WithMessage("{Id} is required");

			if (logger.IsEnabled(LogLevel.Trace))
			{
				logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
			}
		}
	}
}
