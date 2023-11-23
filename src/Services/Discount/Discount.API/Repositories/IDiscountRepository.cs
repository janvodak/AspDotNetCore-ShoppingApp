using ShoppingApp.Services.Discount.API.Models.DataTransferObjects;

namespace ShoppingApp.Services.Discount.API.Repositories
{
	public interface IDiscountRepository
	{
		Task<IEnumerable<DiscountDataTransferObject>> GetDiscountsAsync();

		Task<DiscountDataTransferObject?> GetDiscountByProductNameAsync(string productName);

		Task<int> CreateDiscountAsync(DiscountDataTransferObject discountDataTransferObject);

		Task<int> UpdateDiscountAsync(DiscountDataTransferObject discountDataTransferObject);

		Task<int> DeleteDiscountAsync(string productName);
	}
}
