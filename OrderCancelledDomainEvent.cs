using MediatR;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;

namespace ShoppingApp.Services.Order.API.Domain.Events
{
	public class OrderCancelledDomainEvent : INotification
	{
		public OrderAggregateRoot Order { get; }

		public OrderCancelledDomainEvent(OrderAggregateRoot order)
		{
			Order = order;
		}
	}
}
