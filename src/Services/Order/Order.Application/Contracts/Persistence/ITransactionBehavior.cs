using MediatR;

namespace ShoppingApp.Services.Order.API.Application.Contracts.Persistence
{
	public interface ITransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
	}
}
