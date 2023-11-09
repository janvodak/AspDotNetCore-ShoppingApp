using Shopping.Aggregator.Src.Models;

namespace Shopping.Aggregator.Src.Services
{
	public interface IOrderApiService
	{
		Task<IEnumerable<Order>> GetUserOrders(string username);
	}
}
