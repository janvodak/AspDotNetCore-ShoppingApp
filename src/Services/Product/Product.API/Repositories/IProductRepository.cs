using ShoppingApp.Services.Product.API.Models;

namespace ShoppingApp.Services.Product.API.Repositories
{
	public interface IProductRepository
	{
		Task<IEnumerable<ProductModel>> GetProducts();

		Task<ProductModel> GetProductById(string id);

		Task<IEnumerable<ProductModel>> GetProductsByName(string name);

		Task<IEnumerable<ProductModel>> GetProductsByCategory(string category);

		Task CreateProduct(ProductModel product);

		Task<bool> UpdateProduct(ProductModel product);

		Task<bool> DeleteProduct(string id);
	}
}
