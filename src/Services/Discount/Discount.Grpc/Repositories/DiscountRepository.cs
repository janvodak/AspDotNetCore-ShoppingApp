using Microsoft.EntityFrameworkCore;
using ShoppingApp.Services.Discount.Grpc.Data;
using ShoppingApp.Services.Discount.Grpc.Models;

namespace ShoppingApp.Services.Discount.Grpc.Repositories
{
	public class DiscountRepository : IDiscountRepository
	{
		private readonly DiscountContext _discountContext;
		private readonly ILogger<DiscountRepository> _logger;

		public DiscountRepository(
			DiscountContext discountContext,
			ILogger<DiscountRepository> logger)
		{
			_discountContext = discountContext;
			_logger = logger;
		}

		public async Task<int> CreateDiscountAsync(DiscountModel discountModel)
		{
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

		public async Task<DiscountModel?> GetDiscountByProductNameAsync(string productName)
		{
			return await _discountContext.Discounts.FirstAsync(
				d => d.ProductName.ToLower() == productName.ToLower());
		}

		public async Task<int> UpdateDiscountAsync(DiscountModel discountModel)
		{
			_discountContext.Attach(discountModel).State = EntityState.Modified;

			return await _discountContext.SaveChangesAsync();
		}
	}
}
