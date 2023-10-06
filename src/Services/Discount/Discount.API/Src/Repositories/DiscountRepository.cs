using Discount.API.Src.Data;
using Discount.API.Src.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Src.Repositories
{
	public class DiscountRepository : IDiscountRepository
	{
		private readonly DiscountContext _discountContext;
		private readonly ILogger<DiscountRepository> _logger;

		public DiscountRepository(DiscountContext discountContext, ILogger<DiscountRepository> logger)
		{
			this._discountContext = discountContext;
			this._logger = logger;
		}

		public async Task<bool> CreateDiscount(DiscountEntity discount)
		{
			this._discountContext.Discounts.Add(discount);
			int result = await this._discountContext.SaveChangesAsync();

			if (result == 0)
			{
				string message = $"Unable to create discount: '{discount.ToString}'";
				this._logger.LogError(message: message);

				return false;
			}

			return true;
		}

		public async Task<bool> DeleteDiscount(string productName)
		{
			DiscountEntity? discount = await this._discountContext.Discounts.SingleAsync(d => d.ProductName == productName);

			if (discount == null)
			{
				string message = $"Discount for product: '{productName}' not found";
				this._logger.LogError(message: message);

				return false;
			}

			this._discountContext.Discounts.Remove(discount);

			int result = await this._discountContext.SaveChangesAsync();

			if (result == 0)
			{
				string message = $"Unable to remove discount: '{discount.ToString}'";
				this._logger.LogError(message: message);

				return false;
			}

			return true;
		}

		public async Task<DiscountEntity?> GetDiscount(string productName)
		{
			DiscountEntity? discount = await this._discountContext.Discounts.FirstOrDefaultAsync(d => d.ProductName == productName);

			if (discount == null)
			{
				string message = $"Unable to get discount for product '{productName}'";
				this._logger.LogError(message: message);
			}

			return discount;
		}

		public async Task<bool> UpdateDiscount(DiscountEntity discount)
		{
			this._discountContext.Attach(discount).State = EntityState.Modified;

			try
			{
				int result = await this._discountContext.SaveChangesAsync();

				if (result == 0)
				{
					return false;
				}

				return true;
			}
			catch (Exception exception) when (exception is DbUpdateConcurrencyException || exception is DbUpdateException || exception is OperationCanceledException)
			{
				string message = $"Unable to update discount '{discount.ToString}'.";
				this._logger.LogError(exception, message: message);
			}

			return false;
		}
	}
}
