using System;
using Microsoft.EntityFrameworkCore;
using Order.Application.Src.Contracts.Persistence;
using Order.Domain.Src.Order.Entities;
using Order.Infrastructure.Src.Persistence.Context;

namespace Order.Infrastructure.Src.Persistence.Repositories
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
