namespace ShoppingApp.Services.Order.API.Domain.SeedWork
{
	public interface IUnitOfWork : IDisposable
	{
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
	}
}
