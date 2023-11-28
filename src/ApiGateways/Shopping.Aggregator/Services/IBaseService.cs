using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Services
{
	public interface IBaseService
	{
		Task<HttpResponseMessage> SendAsync(string clientName, RequestDataTransferObject request);
	}
}
