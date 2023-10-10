using Discount.Grpc.Src.Entities;

namespace Discount.Grpc.Src.Repositories
{
	public interface IDiscountRepository
	{
		Task<DiscountEntity?> GetDiscount(string productName);

		Task<bool> CreateDiscount(DiscountEntity discount);

		Task<bool> UpdateDiscount(DiscountEntity discount);

		Task<bool> DeleteDiscount(string productName);
	}
}
