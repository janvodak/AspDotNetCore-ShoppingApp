using Order.Domain.Src.Order.Entities;

namespace Order.Application.Src.Contracts.Persistence
{
	public interface IOrderRepository : IAsyncRepository<OrderEntity>
	{
		Task<IEnumerable<OrderEntity>> GetOrdersByUserName(string userName);
	}
}
