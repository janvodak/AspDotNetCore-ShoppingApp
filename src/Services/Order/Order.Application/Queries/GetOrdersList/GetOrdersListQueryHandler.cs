using MediatR;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Entities;
using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Repositories;

namespace ShoppingApp.Services.Order.API.Application.Queries.GetOrdersList
{
	public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderDataTransferObject>>
	{
		private readonly IOrderRepository _orderRepository;

		public GetOrdersListQueryHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<List<OrderDataTransferObject>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
		{
			List<OrderDataTransferObject> orderDataTransferObjects = new();

			IEnumerable<OrderAggregateRoot> orderAggregates = await _orderRepository.GetOrdersByUserNameAsync(request.UserName);

			foreach (OrderAggregateRoot orderAggregate in orderAggregates)
			{
				OrderDataTransferObject orderDataTransferObject = new				(
					orderAggregate.Id,
					orderAggregate.Customer.UserName,
					orderAggregate.BillingAddress.FirstName,
					orderAggregate.BillingAddress.LastName,
					orderAggregate.BillingAddress.EmailAddress,
					orderAggregate.BillingAddress.AddressLine,
					orderAggregate.BillingAddress.Country,
					orderAggregate.BillingAddress.State,
					orderAggregate.BillingAddress.ZipCode,
					orderAggregate.TotalPrice.ToFloat(),
					orderAggregate.PaymentMethod.Id,
					orderAggregate.PaymentCard.CardName,
					orderAggregate.PaymentCard.CardNumber,
					orderAggregate.PaymentCard.Expiration,
					orderAggregate.PaymentCard.CardVerificationValue
				);

				orderDataTransferObjects.Add(orderDataTransferObject);
			}

			return orderDataTransferObjects;
		}
	}
}
