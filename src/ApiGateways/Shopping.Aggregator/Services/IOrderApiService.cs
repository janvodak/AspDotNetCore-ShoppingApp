using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Services
{
	public interface IOrderApiService
	{
		Task<IEnumerable<OrderDataTransferObject>> GetUserOrdersAsync(string username);
	}
}
