using MediatR;
using ShoppingApp.Services.Order.API.Application.Contracts.Persistence;

namespace ShoppingApp.Services.Order.API.Application.Behaviors
{
	public class TransactionBehaviorDecorator<TRequest, TResponse> : ITransactionBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly ITransactionBehavior<TRequest, TResponse> _transactionBehavior;

		public TransactionBehaviorDecorator(ITransactionBehavior<TRequest, TResponse> transactionBehavior)
		{
			_transactionBehavior = transactionBehavior;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			return await _transactionBehavior.Handle(request, next, cancellationToken);
		}
	}
}
