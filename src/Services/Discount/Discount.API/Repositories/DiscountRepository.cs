using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Services.Discount.API.Data;
using ShoppingApp.Services.Discount.API.Models;
using ShoppingApp.Services.Discount.API.Models.DataTransferObjects;

namespace ShoppingApp.Services.Discount.API.Repositories
{
	public class DiscountRepository : IDiscountRepository
	{
		private readonly DiscountDbContext _discountDbContext;
		private readonly ILogger<DiscountRepository> _logger;
		private readonly IMapper _mapper;

		public DiscountRepository(
			DiscountDbContext discountDbContext,
			ILogger<DiscountRepository> logger,
			IMapper mapper)
		{
			_discountDbContext = discountDbContext;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task<int> CreateDiscountAsync(DiscountDataTransferObject discountDataTransferObject)
		{
			DiscountModel discountModel = _mapper.Map<DiscountModel>(discountDataTransferObject);

			_discountDbContext.Discounts.Add(discountModel);

			return await _discountDbContext.SaveChangesAsync();
		}

		public async Task<int> DeleteDiscountAsync(string productName)
		{
			DiscountModel? discountModel = await _discountDbContext.Discounts.FirstAsync(
				d => d.ProductName.ToLower() == productName.ToLower());

			if (discountModel == null)
			{
				_logger.LogError(
					"Discount for product: '{ProductName}' not found.",
					productName);

				throw new Exception();
			}

			_discountDbContext.Discounts.Remove(discountModel);

			return await _discountDbContext.SaveChangesAsync();
		}

		public async Task<DiscountDataTransferObject?> GetDiscountByProductNameAsync(string productName)
		{
			DiscountModel discountModel = await _discountDbContext.Discounts.FirstAsync(
				d => d.ProductName.ToLower() == productName.ToLower());

			return _mapper.Map<DiscountDataTransferObject>(discountModel);

		}

		public async Task<IEnumerable<DiscountDataTransferObject>> GetDiscountsAsync()
		{
			IEnumerable<DiscountModel> discountModels = await _discountDbContext.Discounts.ToListAsync();

			return _mapper.Map<IEnumerable<DiscountDataTransferObject>>(discountModels);
		}

		public async Task<int> UpdateDiscountAsync(DiscountDataTransferObject discountDataTransferObject)
		{
			DiscountModel discountModel = _mapper.Map<DiscountModel>(discountDataTransferObject);

			_discountDbContext.Attach(discountModel).State = EntityState.Modified;

			return await _discountDbContext.SaveChangesAsync();
		}
	}
}
