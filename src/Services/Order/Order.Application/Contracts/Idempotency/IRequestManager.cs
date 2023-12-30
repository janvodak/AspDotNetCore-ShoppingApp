namespace ShoppingApp.Services.Order.API.Application.Contracts.Idempotency
{
	public interface IRequestManager
	{
		Task<bool> ExistAsync(Guid id);

		Task CreateRequestForCommandAsync<T>(Guid id);
	}
}
