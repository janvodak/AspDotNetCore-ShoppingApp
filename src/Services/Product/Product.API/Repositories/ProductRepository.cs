using MongoDB.Driver;
using ShoppingApp.Services.Product.API.Data;
using ShoppingApp.Services.Product.API.Models;

namespace ShoppingApp.Services.Product.API.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly IMongoCollection<ProductModel> _productCollection;

		public ProductRepository(IProductDbContext catalogContext)
		{
			_productCollection = catalogContext.GetProductMongoCollection();
		}

		public async Task CreateProduct(ProductModel product)
		{
			await _productCollection.InsertOneAsync(product);
		}

		public async Task<bool> DeleteProduct(string id)
		{
			FilterDefinition<ProductModel> filter = Builders<ProductModel>.Filter.Eq(p => p.Id, id);

			DeleteResult result = await _productCollection.DeleteOneAsync(filter);

			return result.IsAcknowledged == true
				&& result.DeletedCount > 0;
		}

		public async Task<ProductModel> GetProductById(string id)
		{
			return await _productCollection
				.Find(p => p.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<ProductModel>> GetProducts()
		{
			return await _productCollection.Find(p => true).ToListAsync();
		}

		public async Task<IEnumerable<ProductModel>> GetProductsByCategory(string category)
		{
			FilterDefinition<ProductModel> filter = Builders<ProductModel>.Filter.Eq(p => p.Category, category);

			return await _productCollection
				.Find(filter)
				.ToListAsync();
		}

		public async Task<IEnumerable<ProductModel>> GetProductsByName(string name)
		{
			FilterDefinition<ProductModel> definition = Builders<ProductModel>.Filter.Eq(p => p.Name, name);

			return await _productCollection.Find(definition)
				.ToListAsync();
		}

		public async Task<bool> UpdateProduct(ProductModel product)
		{
			var result = await _productCollection
				.ReplaceOneAsync(
					filter: g => g.Id == product.Id,
					replacement: product
				);

			return result.IsAcknowledged == true
				&& result.ModifiedCount > 0;
		}
	}
}
