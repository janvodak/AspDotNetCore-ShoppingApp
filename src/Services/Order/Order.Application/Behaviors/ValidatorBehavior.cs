using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Extensions;
using ShoppingApp.Services.Order.API.Domain.Exceptions;
using ValidationException = ShoppingApp.Services.Order.API.Application.Exceptions.ValidationException;

namespace ShoppingApp.Services.Order.API.Application.Behaviors
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
			string typeName = request.GetGenericTypeName();

			_logger.LogInformation("Validating command {CommandType}", typeName);

			List<ValidationFailure> failures = _validators
				.Select(v => v.Validate(request))
				.SelectMany(result => result.Errors)
				.Where(error => error != null)
				.ToList();

			if (failures.Any() == true)
			{
				_logger.LogWarning(
					"Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}",
					typeName,
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
