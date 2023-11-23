using ShoppingApp.Services.Discount.Grpc.Models;

namespace ShoppingApp.Services.Discount.Grpc.Repositories
{
	public interface IDiscountRepository
	{
		Task<DiscountModel?> GetDiscountByProductNameAsync(string productName);

		Task<int> CreateDiscountAsync(DiscountModel discountModel);

		Task<int> UpdateDiscountAsync(DiscountModel discountModel);

		Task<int> DeleteDiscountAsync(string productName);
	}
}
