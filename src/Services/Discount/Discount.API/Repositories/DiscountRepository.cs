using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Services.Discount.API.Data;
using ShoppingApp.Services.Discount.API.Models;
using ShoppingApp.Services.Discount.API.Models.DataTransferObjects;

namespace ShoppingApp.Services.Discount.API.Repositories
{
	public class DiscountRepository : IDiscountRepository
	{
		private readonly DiscountContext _discountContext;
		private readonly ILogger<DiscountRepository> _logger;
		private readonly IMapper _mapper;

		public DiscountRepository(
			DiscountContext discountContext,
			ILogger<DiscountRepository> logger,
			IMapper mapper)
		{
			_discountContext = discountContext;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task<int> CreateDiscountAsync(DiscountDataTransferObject discountDataTransferObject)
		{
			DiscountModel discountModel = _mapper.Map<DiscountModel>(discountDataTransferObject);

			_discountContext.Discounts.Add(discountModel);

			return await _discountContext.SaveChangesAsync();
		}

		public async Task<int> DeleteDiscountAsync(string productName)
		{
			DiscountModel? discountModel = await _discountContext.Discounts.FirstAsync(
				d => d.ProductName.ToLower() == productName.ToLower());

			if (discountModel == null)
			{
				_logger.LogError(
					"Discount for product: '{ProductName}' not found.",
					productName);

				throw new Exception();
			}

			_discountContext.Discounts.Remove(discountModel);

			return await _discountContext.SaveChangesAsync();
		}

		public async Task<DiscountDataTransferObject?> GetDiscountByProductNameAsync(string productName)
		{
			DiscountModel discountModel = await _discountContext.Discounts.FirstAsync(
				d => d.ProductName.ToLower() == productName.ToLower());

			return _mapper.Map<DiscountDataTransferObject>(discountModel);

		}

		public async Task<IEnumerable<DiscountDataTransferObject>> GetDiscountsAsync()
		{
			IEnumerable<DiscountModel> discountModels = await _discountContext.Discounts.ToListAsync();

			return _mapper.Map<IEnumerable<DiscountDataTransferObject>>(discountModels);
		}

		public async Task<int> UpdateDiscountAsync(DiscountDataTransferObject discountDataTransferObject)
		{
			DiscountModel discountModel = _mapper.Map<DiscountModel>(discountDataTransferObject);

			_discountContext.Attach(discountModel).State = EntityState.Modified;

			return await _discountContext.SaveChangesAsync();
		}
	}
}
