using MediatR;
using Microsoft.Extensions.Logging;

namespace ShoppingApp.Services.Order.API.Application.Behaviours
{
	public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly ILogger<TRequest> _logger;

		public UnhandledExceptionBehavior(ILogger<TRequest> logger)
		{
			_logger = logger;
		}

		public async Task<TResponse> Handle(
			TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken)
		{
			try
			{
				return await next();
			}
			catch (Exception exception)
			{
				var requestName = typeof(TRequest).Name;

				_logger.LogError(
					exception,
					"Unhandled Exception for Request '{Name}'. Application Request: {@Request}",
					requestName,
					request);

				throw;
			}
		}
	}
}
