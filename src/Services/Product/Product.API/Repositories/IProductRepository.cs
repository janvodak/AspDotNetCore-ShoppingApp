using ShoppingApp.Services.Product.API.Models.DataTransferObjects;

namespace ShoppingApp.Services.Product.API.Repositories
{
	public interface IProductRepository
	{
		Task<IEnumerable<ProductDataTransferObject>> GetProductsAsync();

		Task<ProductDataTransferObject?> GetProductByIdAsync(string id);

		Task<IEnumerable<ProductDataTransferObject>> GetProductsByNameAsync(string name);

		Task<IEnumerable<ProductDataTransferObject>> GetProductsByCategoryAsync(string category);

		Task CreateProductAsync(ProductDataTransferObject productDataTransferObject);

		Task<bool> UpdateProductAsync(ProductDataTransferObject productDataTransferObject);

		Task<bool> DeleteProductAsync(string id);
	}
}
