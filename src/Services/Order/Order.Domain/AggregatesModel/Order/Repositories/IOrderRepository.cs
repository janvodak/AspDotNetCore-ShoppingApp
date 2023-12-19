using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.SeedWork;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Repositories
{
	public interface IOrderRepository : IAsyncRepository<OrderAggregateRoot>
	{
		Task<IEnumerable<OrderAggregateRoot>> GetOrdersByUserName(string userName);
	}
}
