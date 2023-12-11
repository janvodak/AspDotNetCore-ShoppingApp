using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features.Handlers
{
	public interface IBasketHandler
	{
		Task<BasketDataTransferObject> Handle(string userName);
	}
}
