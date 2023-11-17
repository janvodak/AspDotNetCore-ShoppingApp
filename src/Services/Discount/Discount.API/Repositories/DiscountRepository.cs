using Microsoft.EntityFrameworkCore;
using ShoppingApp.Services.Discount.API.Data;
using ShoppingApp.Services.Discount.API.Models;

namespace ShoppingApp.Services.Discount.API.Repositories
{
	public class DiscountRepository : IDiscountRepository
	{
		private readonly DiscountContext _discountContext;
		private readonly ILogger<DiscountRepository> _logger;

		public DiscountRepository(DiscountContext discountContext, ILogger<DiscountRepository> logger)
		{
			_discountContext = discountContext;
			_logger = logger;
		}

		public async Task<bool> CreateDiscount(DiscountModel discount)
		{
			_discountContext.Discounts.Add(discount);
			int result = await _discountContext.SaveChangesAsync();

			if (result == 0)
			{
				_logger.LogError("Unable to create discount: '{Discount}'", discount.ToString());

				return false;
			}

			return true;
		}

		public async Task<bool> DeleteDiscount(string productName)
		{
			DiscountModel? discount = await _discountContext.Discounts.SingleAsync(d => d.ProductName == productName);

			if (discount == null)
			{
				string message = $"Discount for product: '{productName}' not found";
				_logger.LogError(message);

				return false;
			}

			_discountContext.Discounts.Remove(discount);

			int result = await _discountContext.SaveChangesAsync();

			if (result == 0)
			{
				_logger.LogError("Unable to remove discount: '{Discount}'", discount.ToString());

				return false;
			}

			return true;
		}

		public async Task<DiscountModel?> GetDiscount(string productName)
		{
			DiscountModel? discount = await _discountContext.Discounts.FirstOrDefaultAsync(d => d.ProductName == productName);

			if (discount == null)
			{
				_logger.LogError("Unable to get discount for product '{ProductName}'", productName);
			}

			return discount;
		}

		public async Task<bool> UpdateDiscount(DiscountModel discount)
		{
			_discountContext.Attach(discount).State = EntityState.Modified;

			try
			{
				int result = await _discountContext.SaveChangesAsync();

				if (result == 0)
				{
					return false;
				}

				return true;
			}
			catch (Exception exception) when (exception is DbUpdateConcurrencyException || exception is DbUpdateException || exception is OperationCanceledException)
			{
				_logger.LogError(
					exception,
					"Unable to update discount '{Discount}'.",
					discount.ToString());
			}

			return false;
		}
	}
}
