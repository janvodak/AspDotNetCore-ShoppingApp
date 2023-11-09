using Shopping.Aggregator.Src.Models;

namespace Shopping.Aggregator.Src.Services
{
	public interface IProductApiService
	{
		Task<IEnumerable<Product>> GetProducts();

		Task<IEnumerable<Product>> GetProductsByCategory(string category);

		Task<Product> GetProductById(string id);
	}
}
