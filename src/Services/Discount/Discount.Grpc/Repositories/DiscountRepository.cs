using Microsoft.EntityFrameworkCore;
using ShoppingApp.Services.Discount.Grpc.Data;
using ShoppingApp.Services.Discount.Grpc.Models;

namespace ShoppingApp.Services.Discount.Grpc.Repositories
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
				string errorMessage = $"Unable to create discount: '{discount.ToString}'.";
				_logger.LogError(message: errorMessage);

				return false;
			}


			string message = $"Discount for product '{discount.ProductName}' with amount {discount.Amount} was created successfully.";
			_logger.LogInformation(message: message);

			return true;
		}

		public async Task<bool> DeleteDiscount(string productName)
		{
			DiscountModel? discount = await _discountContext.Discounts.SingleAsync(d => d.ProductName == productName);

			if (discount == null)
			{
				string errorMessage = $"Discount for product: '{productName}' not found";
				_logger.LogError(message: errorMessage);

				return false;
			}

			_discountContext.Discounts.Remove(discount);

			int result = await _discountContext.SaveChangesAsync();

			if (result == 0)
			{
				string errorMessage = $"Unable to remove discount: '{discount.ToString}'";
				_logger.LogError(message: errorMessage);

				return false;
			}

			string message = $"Discount for product '{discount.ProductName}' with amount {discount.Amount} was removed successfully.";
			_logger.LogInformation(message: message);

			return true;
		}

		public async Task<DiscountModel?> GetDiscount(string productName)
		{
			DiscountModel? discount = await _discountContext.Discounts.FirstOrDefaultAsync(d => d.ProductName == productName);

			if (discount == null)
			{
				string errorMessage = $"Unable to get discount for product '{productName}'";
				_logger.LogError(message: errorMessage);

				return null;
			}

			string message = $"Discount for product '{discount.ProductName}' and amount {discount.Amount} was returned successfully.";
			_logger.LogInformation(message: message);

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

				string message = $"Discount for product '{discount.ProductName}' and amount {discount.Amount} was updated successfully.";
				_logger.LogInformation(message: message);

				return true;
			}
			catch (Exception exception) when (exception is DbUpdateConcurrencyException || exception is DbUpdateException || exception is OperationCanceledException)
			{
				string message = $"Unable to update discount '{discount.ToString}'.";
				_logger.LogError(exception, message: message);
			}

			return false;
		}
	}
}
