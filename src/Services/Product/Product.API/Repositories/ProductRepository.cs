using AutoMapper;
using MongoDB.Driver;
using ShoppingApp.Services.Product.API.Data;
using ShoppingApp.Services.Product.API.Models;
using ShoppingApp.Services.Product.API.Models.DataTransferObjects;

namespace ShoppingApp.Services.Product.API.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly IMongoCollection<ProductModel> _productCollection;
		private readonly IMapper _mapper;

		public ProductRepository(
			IProductDbContext catalogContext,
			IMapper mapper)
		{
			_productCollection = catalogContext.GetProductMongoCollection();
			_mapper = mapper;
		}

		public async Task CreateProductAsync(ProductDataTransferObject productDataTransferObject)
		{
			ProductModel productModel = _mapper.Map<ProductModel>(productDataTransferObject);

			await _productCollection.InsertOneAsync(productModel);
		}

		public async Task<bool> DeleteProductAsync(string id)
		{
			FilterDefinition<ProductModel> filter = Builders<ProductModel>.Filter.Eq(p => p.Id, id);

			DeleteResult result = await _productCollection.DeleteOneAsync(filter);

			return result.IsAcknowledged == true
				&& result.DeletedCount > 0;
		}

		public async Task<ProductDataTransferObject?> GetProductByIdAsync(string id)
		{
			ProductModel productModel = await _productCollection
				.Find(p => p.Id == id)
				.FirstAsync();

			return _mapper.Map<ProductDataTransferObject>(productModel);
		}

		public async Task<IEnumerable<ProductDataTransferObject>> GetProductsAsync()
		{
			IEnumerable<ProductModel> productModels = await _productCollection.Find(p => true).ToListAsync();

			return _mapper.Map<IEnumerable<ProductDataTransferObject>>(productModels);
		}

		public async Task<IEnumerable<ProductDataTransferObject>> GetProductsByCategoryAsync(string category)
		{
			FilterDefinition<ProductModel> filter = Builders<ProductModel>.Filter.Eq(p => p.Category, category);

			IEnumerable<ProductModel> productModels = await _productCollection
				.Find(filter)
				.ToListAsync();

			return _mapper.Map<IEnumerable<ProductDataTransferObject>>(productModels);
		}

		public async Task<IEnumerable<ProductDataTransferObject>> GetProductsByNameAsync(string name)
		{
			FilterDefinition<ProductModel> definition = Builders<ProductModel>.Filter.Eq(p => p.Name, name);

			IEnumerable<ProductModel> productModels = await _productCollection.Find(definition)
				.ToListAsync();

			return _mapper.Map<IEnumerable<ProductDataTransferObject>>(productModels);
		}

		public async Task<bool> UpdateProductAsync(ProductDataTransferObject productDataTransferObject)
		{
			ProductModel productModel = _mapper.Map<ProductModel>(productDataTransferObject);

			var result = await _productCollection
				.ReplaceOneAsync(
					filter: g => g.Id == productModel.Id,
					replacement: productModel
				);

			return result.IsAcknowledged == true
				&& result.ModifiedCount > 0;
		}
	}
}
