using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Services
{
	public interface IProductApiService
	{
		Task<IEnumerable<ProductDataTransferObject>> GetProductsAsync();

		Task<IEnumerable<ProductDataTransferObject>> GetProductsByCategoryAsync(string category);

		Task<ProductDataTransferObject> GetProductByIdAsync(string id);
	}
}
