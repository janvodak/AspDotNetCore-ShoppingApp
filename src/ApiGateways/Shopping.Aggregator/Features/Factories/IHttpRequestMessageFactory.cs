using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features.Factories
{
	public interface IHttpRequestMessageFactory
	{
		HttpRequestMessage Create(RequestDataTransferObject request);
	}
}
