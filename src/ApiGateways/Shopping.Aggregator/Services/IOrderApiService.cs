using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Services
{
	public interface IOrderApiService
	{
		Task<ResponseDataTransferObject<IEnumerable<OrderDataTransferObject>>> GetUserOrdersAsync(string username);
	}
}
