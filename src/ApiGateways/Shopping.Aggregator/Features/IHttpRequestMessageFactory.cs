using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features
{
	public interface IHttpRequestMessageFactory
	{
		HttpRequestMessage Create(RequestDataTransferObject request);
	}
}
