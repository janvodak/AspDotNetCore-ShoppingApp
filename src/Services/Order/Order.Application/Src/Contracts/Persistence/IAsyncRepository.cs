﻿using System.Linq.Expressions;
using Order.Domain.Src.Shared;

namespace Order.Application.Src.Contracts.Persistence
{
	public interface IAsyncRepository<T> where T : EntityBase
	{
		Task<IReadOnlyList<T>> GetAllAsync();

		Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

		Task<IReadOnlyList<T>> GetAsync(
			Expression<Func<T, bool>>? predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			string? includeString = null,
			bool disableTracking = true);

		Task<IReadOnlyList<T>> GetAsync(
			Expression<Func<T, bool>>? predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			List<Expression<Func<T, object>>>? includes = null,
			bool disableTracking = true);

		Task<T?> GetByIdAsync(int id);

		Task<T> AddAsync(T entity);

		Task UpdateAsync(T entity);

		Task DeleteAsync(T entity);
	}
}
