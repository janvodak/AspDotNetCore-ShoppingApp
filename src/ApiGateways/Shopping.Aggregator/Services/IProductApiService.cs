using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Services
{
	public interface IProductApiService
	{
		Task<ResponseDataTransferObject<IEnumerable<ProductDataTransferObject>>> GetProductsAsync();

		Task<ResponseDataTransferObject<IEnumerable<ProductDataTransferObject>>> GetProductsByCategoryAsync(string category);

		Task<ResponseDataTransferObject<ProductDataTransferObject>> GetProductByIdAsync(string id);
	}
}
