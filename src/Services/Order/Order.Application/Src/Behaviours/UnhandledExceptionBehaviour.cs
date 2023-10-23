using MediatR;
using Microsoft.Extensions.Logging;

namespace Order.Application.Src.Behaviours
{
	public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly ILogger<TRequest> _logger;

		public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
		{
			this._logger = logger;
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

				this._logger.LogError(
					exception,
					"Unhandled Exception for Request '{Name}'. Application Request: {@Request}",
					requestName,
					request);

				throw;
			}
		}
	}
}
