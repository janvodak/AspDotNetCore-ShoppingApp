using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingApp.Services.Order.API.Domain.SeedWork;
using ShoppingApp.Services.Order.API.Infrastructure.Persistence.Context;

namespace ShoppingApp.Services.Order.API.Infrastructure.Persistence.Extensions
{
	static class MediatorExtension
	{
		public static async Task DispatchDomainEventsAsync(this IMediator mediator, OrderContext context)
		{
			IEnumerable<EntityEntry<EntityBase>> domainEntities = context.ChangeTracker
				.Entries<EntityBase>()
				.Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

			List<INotification> domainEvents = domainEntities
				.SelectMany(x => x.Entity.DomainEvents)
				.ToList();

			domainEntities.ToList()
				.ForEach(entity => entity.Entity.ClearDomainEvents());

			foreach (var domainEvent in domainEvents)
				await mediator.Publish(domainEvent);
		}
	}
}
