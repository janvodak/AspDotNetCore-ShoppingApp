using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services
{
	public interface IOrderApiService
	{
		Task<IEnumerable<Order>> GetUserOrders(string username);
	}
}

