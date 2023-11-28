using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Services
{
	public interface IBasketApiService
	{
		Task<BasketDataTransferObject> GetBasketAsync(string username);
	}
}
