using ShoppingApp.Services.Order.API.Application.Contracts.Idempotency;
using ShoppingApp.Services.Order.API.Domain.Exceptions;
using ShoppingApp.Services.Order.API.Infrastructure.Idempotency.DataTransferObjects;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Context;

namespace ShoppingApp.Services.Order.API.Infrastructure.Idempotency.Services
{
	public class RequestManager : IRequestManager
	{
		private readonly OrderContext _context;

		public RequestManager(OrderContext context)
		{
			_context = context;
		}

		public async Task<bool> ExistAsync(Guid id)
		{
			ClientRequest? request = await _context.FindAsync<ClientRequest>(id);

			return request != null;
		}

		public async Task CreateRequestForCommandAsync<T>(Guid id)
		{
			bool isExists = await ExistAsync(id);

			if (isExists == true)
			{
				throw new DomainException($"Request with {id} already exists");
			}

			_context.Add(
				new ClientRequest()
				{
					Id = id,
					Name = typeof(T).Name,
					Time = DateTime.UtcNow
				}
			);

			await _context.SaveChangesAsync();
		}
	}
}
