using Shopping.Aggregator.Src.Models;

namespace Shopping.Aggregator.Src.Services
{
	public interface IProductApiService
	{
		Task<IEnumerable<Product>> GetProductsAsync();

		Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);

		Task<Product> GetProductByIdAsync(string id);
	}
}
