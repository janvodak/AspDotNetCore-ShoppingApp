using MediatR;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;

namespace ShoppingApp.Services.Order.API.Domain.Events
{
	public class OrderCreatedDomainEvent : INotification
	{
		public OrderAggregateRoot Order { get; }

		public OrderCreatedDomainEvent(OrderAggregateRoot order)
		{
			Order = order;
		}
	}
}
