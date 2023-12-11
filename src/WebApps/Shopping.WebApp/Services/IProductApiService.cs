using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services
{
	public interface IProductApiService
	{
		Task<IEnumerable<Product>> GetProducts();

		Task<IEnumerable<Product>> GetProductsByCategory(string category);

		Task<Product?> GetProductById(string id);

		Task<Product?> CreateProduct(Product	product);
	}
}
