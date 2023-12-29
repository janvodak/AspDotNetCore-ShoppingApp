using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Extensions;
using ShoppingApp.Services.Order.API.Domain.Exceptions;
using ValidationException = ShoppingApp.Services.Order.API.Application.Exceptions.ValidationException;

namespace ShoppingApp.Services.Order.API.Application.Behaviours
{
	public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;
		private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;

		public ValidatorBehavior(
			IEnumerable<IValidator<TRequest>> validators,
			ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
		{
			_validators = validators;
			_logger = logger;
		}

		public async Task<TResponse> Handle(
			TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken)
		{
			if (_validators.Any() == false)
			{
				return await next();
			}

			ValidationContext<TRequest> context = new(request);

			ValidationResult[] validationResults = await Task.WhenAll(
				_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

			List<ValidationFailure> failures = validationResults
				.SelectMany(r => r.Errors)
				.Where(f => f != null).ToList();

			if (failures.Any() == true)
			{
				_logger.LogWarning(
					"Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}",
					request.GetGenericTypeName(),
					request,
					failures);

				throw new DomainException(
					$"Command Validation Errors for type {typeof(TRequest).Name}",
					new ValidationException(failures));
			}

			return await next();
		}
	}
}
