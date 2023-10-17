using FluentValidation;

namespace Order.Application.Src.Features.Order.Commands.UpdateOrder
{
	public class UpdateOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
	{
		public UpdateOrderCommandValidator()
		{
			RuleFor(p => p.UserName)
				.NotEmpty().WithMessage("{UserName} is required.")
				.NotNull()
				.MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

			RuleFor(p => p.EmailAddress)
				.NotEmpty().WithMessage("{EmailAddress} is required.");

			RuleFor(p => p.TotalPrice)
				.NotEmpty().WithMessage("{TotalPrice} is required.")
				.GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");
		}
	}
}
