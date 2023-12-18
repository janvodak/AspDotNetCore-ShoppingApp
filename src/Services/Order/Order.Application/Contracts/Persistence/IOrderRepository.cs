using ShoppingApp.Services.Order.API.Domain.Order;

namespace ShoppingApp.Services.Order.API.Application.Contracts.Persistence
{
	public interface IOrderRepository : IAsyncRepository<OrderEntity>
	{
		Task<IEnumerable<OrderEntity>> GetOrdersByUserName(string userName);
	}
}
