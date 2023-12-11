using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features.Handlers
{
	public interface IProductHandler
	{
		Task<ProductDataTransferObject?> Handle(string id);
	}
}