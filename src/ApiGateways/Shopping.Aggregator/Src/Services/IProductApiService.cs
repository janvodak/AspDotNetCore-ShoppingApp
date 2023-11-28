using Shopping.Aggregator.Src.Models.DataTransferObjects;

namespace Shopping.Aggregator.Src.Services
{
	public interface IProductApiService
	{
		Task<IEnumerable<ProductDataTransferObject>> GetProductsAsync();

		Task<IEnumerable<ProductDataTransferObject>> GetProductsByCategoryAsync(string category);

		Task<ProductDataTransferObject> GetProductByIdAsync(string id);
	}
}
