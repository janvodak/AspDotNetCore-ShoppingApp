using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Commands.CancelOrder;
using ShoppingApp.Services.Order.API.Application.Commands.CreateOrder;
using ShoppingApp.Services.Order.API.Application.Contracts.Idempotency;
using ShoppingApp.Services.Order.API.Application.Extensions;

namespace ShoppingApp.Services.Order.API.Application.Commands.Shared
{
	/// <summary>
	/// Provides a base implementation for handling duplicate request and ensuring idempotent updates, in the cases where
	/// a requestid sent by client is used to detect duplicate requests.
	/// </summary>
	/// <typeparam name="T">Type of the command handler that performs the operation if request is not duplicated</typeparam>
	/// <typeparam name="R">Return value of the inner command handler</typeparam>
	public abstract class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R> where T : IRequest<R>
	{
		private readonly IMediator _mediator;
		private readonly IRequestManager _requestManager;
		private readonly ILogger<IdentifiedCommandHandler<T, R>> _logger;

		public IdentifiedCommandHandler(
			IMediator mediator,
			IRequestManager requestManager,
			ILogger<IdentifiedCommandHandler<T, R>> logger)
		{
			ArgumentNullException.ThrowIfNull(logger);

			_mediator = mediator;
			_requestManager = requestManager;
			_logger = logger;
		}

		/// <summary>
		/// This method handles the command. It just ensures that no other request exists with the same ID, and if this is the case
		/// just enqueues the original inner command.
		/// </summary>
		/// <param name="message">IdentifiedCommand which contains both original command & request ID</param>
		/// <returns>Return value of inner command or default value if request same ID was found</returns>
		public async Task<R> Handle(IdentifiedCommand<T, R> message, CancellationToken cancellationToken)
		{
			bool isAlreadyExists = await _requestManager.ExistAsync(message.Id);

			if (isAlreadyExists == true)
			{
				return CreateResultForDuplicateRequest();
			}

			await _requestManager.CreateRequestForCommandAsync<T>(message.Id);

			T command = message.Command;
			string commandName = command.GetGenericTypeName();
			string idProperty = string.Empty;
			string commandId = string.Empty;

			switch (command)
			{
				case CreateOrderCommand createOrderCommand:
					idProperty = nameof(createOrderCommand.EmailAddress);
					commandId = createOrderCommand.EmailAddress;
					break;
				case CancelOrderCommand cancelOrderCommand:
					idProperty = nameof(cancelOrderCommand.Id);
					commandId = $"{cancelOrderCommand.Id}";
					break;
				default:
					idProperty = "Id?";
					commandId = "n/a";
					break;
			}

			_logger.LogInformation(
				"Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
				commandName,
				idProperty,
				commandId,
				command);

			R? result;

			try
			{
				// Send the embedded business command to mediator so it runs its related CommandHandler 
				result = await _mediator.Send(command, cancellationToken);
			}
			catch
			{
				return default;
			}

			_logger.LogInformation(
				"Command result: {@Result} - {CommandName} - {IdProperty}: {CommandId} ({@Command})",
				result,
				commandName,
				idProperty,
				commandId,
				command);

			return result;
		}

		/// <summary>
		/// Creates the result value to return if a previous request was found
		/// </summary>
		/// <returns></returns>
		protected abstract R CreateResultForDuplicateRequest();
	}
}
