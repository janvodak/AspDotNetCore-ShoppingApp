using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Contracts.Persistence;
using ShoppingApp.Services.Order.API.Application.Extensions;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Context;

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Behaviors
{
	public class TransactionBehavior<TRequest, TResponse> : ITransactionBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
		private readonly OrderContext _dbContext;

		public TransactionBehavior(
			ILogger<TransactionBehavior<TRequest, TResponse>> logger,
			OrderContext dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			var response = default(TResponse);
			var typeName = request.GetGenericTypeName();

			try
			{
				if (_dbContext.HasActiveTransaction)
				{
					return await next();
				}

				var strategy = _dbContext.Database.CreateExecutionStrategy();

				await strategy.ExecuteAsync(async () =>
				{
					Guid transactionId;

					await using IDbContextTransaction? transaction = await _dbContext.BeginTransactionAsync();

					using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("TransactionContext", transaction!.TransactionId) }))
					{
						_logger.LogInformation("Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

						response = await next();

						_logger.LogInformation("Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

						await _dbContext.CommitTransactionAsync(transaction);

						transactionId = transaction.TransactionId;
					}
				});

				return response!;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error Handling transaction for {CommandName} ({@Command})", typeName, request);

				throw;
			}
		}
	}
}
