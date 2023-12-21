using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Repositories;
using ShoppingApp.Services.Order.API.Domain.SeedWork;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Context;

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		protected readonly OrderContext _dbContext;

		public OrderRepository(OrderContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IUnitOfWork UnitOfWork => _dbContext;

		public async Task<OrderAggregateRoot> AddAsync(OrderAggregateRoot entity)
		{
			return (await _dbContext.Orders.AddAsync(entity)).Entity;
		}

		public void Delete(OrderAggregateRoot entity)
		{
			_dbContext.Orders.Remove(entity);
		}

		public async Task<IReadOnlyList<OrderAggregateRoot>> GetAllAsync()
		{
			return await _dbContext.Orders.ToListAsync();
		}

		public async Task<IReadOnlyList<OrderAggregateRoot>> GetAsync(Expression<Func<OrderAggregateRoot, bool>> predicate)
		{
			return await _dbContext.Orders.Where(predicate).ToListAsync();
		}

		public async Task<IReadOnlyList<OrderAggregateRoot>> GetAsync(
			Expression<Func<OrderAggregateRoot, bool>>? predicate = null,
			Func<IQueryable<OrderAggregateRoot>, IOrderedQueryable<OrderAggregateRoot>>? orderBy = null,
			string? includeString = null,
			bool disableTracking = true)
		{
			IQueryable<OrderAggregateRoot> query = _dbContext.Orders.AsQueryable();

			if (disableTracking == true)
			{
				query = query.AsNoTracking();
			}

			if (string.IsNullOrWhiteSpace(includeString) == false)
			{
				query = query.Include(includeString);
			}

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (orderBy != null)
			{
				return await orderBy(query).ToListAsync();
			}

			return await query.ToListAsync();
		}

		public async Task<IReadOnlyList<OrderAggregateRoot>> GetAsync(
			Expression<Func<OrderAggregateRoot, bool>>? predicate = null,
			Func<IQueryable<OrderAggregateRoot>, IOrderedQueryable<OrderAggregateRoot>>? orderBy = null,
			List<Expression<Func<OrderAggregateRoot, object>>>? includes = null,
			bool disableTracking = true)
		{
			IQueryable<OrderAggregateRoot> query = _dbContext.Orders.AsQueryable();

			if (disableTracking == true)
			{
				query = query.AsNoTracking();
			}

			if (includes != null)
			{
				query = includes.Aggregate(query, (current, include) => current.Include(include));
			}

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (orderBy != null)
			{
				return await orderBy(query).ToListAsync();
			}

			return await query.ToListAsync();
		}

		public async Task<OrderAggregateRoot?> GetByIdAsync(int id)
		{
			OrderAggregateRoot? order = await _dbContext.Orders.FindAsync(id);

			return order;
		}

		public async Task<IEnumerable<OrderAggregateRoot>> GetOrdersByUserNameAsync(string userName)
		{
			return await _dbContext.Orders
				.Where(o => o.UserName == userName)
				.ToListAsync();
		}

		public void Update(OrderAggregateRoot entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
		}
	}
}
