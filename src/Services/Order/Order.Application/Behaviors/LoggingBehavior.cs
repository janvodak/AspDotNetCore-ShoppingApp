﻿using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Extensions;

namespace ShoppingApp.Services.Order.API.Application.Behaviors
{
	public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

		public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
		{
			_logger = logger;
		}

		public async Task<TResponse> Handle(
			TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken)
		{
			_logger.LogInformation(
				"Handling command {CommandName} ({@Command})",
				request.GetGenericTypeName(),
				request);

			TResponse? response = await next();

			_logger.LogInformation(
				"Command {CommandName} handled - response: {@Response}",
				request.GetGenericTypeName(),
				response);

			return response;
		}
	}
}