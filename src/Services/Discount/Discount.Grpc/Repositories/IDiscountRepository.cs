using ShoppingApp.Services.Discount.Grpc.Models;

namespace ShoppingApp.Services.Discount.Grpc.Repositories
{
	public interface IDiscountRepository
	{
		Task<DiscountModel?> GetDiscount(string productName);

		Task<bool> CreateDiscount(DiscountModel discount);

		Task<bool> UpdateDiscount(DiscountModel discount);

		Task<bool> DeleteDiscount(string productName);
	}
}
