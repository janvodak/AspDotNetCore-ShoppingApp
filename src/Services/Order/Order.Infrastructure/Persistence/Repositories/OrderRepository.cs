using Microsoft.EntityFrameworkCore;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Repositories;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Context;

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Repositories
{
	public class OrderRepository : RepositoryBase<OrderAggregateRoot>, IOrderRepository
	{
		public OrderRepository(OrderContext dbContext) : base(dbContext)
		{
		}

		public async Task<IEnumerable<OrderAggregateRoot>> GetOrdersByUserName(string userName)
		{
			return await _dbContext.Orders
				.Where(o => o.UserName == userName)
				.ToListAsync();
		}
	}
}
