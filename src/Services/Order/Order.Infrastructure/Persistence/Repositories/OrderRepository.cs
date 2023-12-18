using Microsoft.EntityFrameworkCore;
using ShoppingApp.Services.Order.API.Application.Contracts.Persistence;
using ShoppingApp.Services.Order.API.Domain.Order;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Context;

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Repositories
{
	public class OrderRepository : RepositoryBase<OrderEntity>, IOrderRepository
	{
		public OrderRepository(OrderContext dbContext) : base(dbContext)
		{
		}

		public async Task<IEnumerable<OrderEntity>> GetOrdersByUserName(string userName)
		{
			return await _dbContext.Orders
				.Where(o => o.UserName == userName)
				.ToListAsync();
		}
	}
}
