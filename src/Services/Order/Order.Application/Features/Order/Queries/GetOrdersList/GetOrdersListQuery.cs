using MediatR;

namespace ShoppingApp.Services.Order.API.Application.Features.Order.Queries.GetOrdersList
{
	public class GetOrdersListQuery : IRequest<List<OrderDataTransferObject>>
	{
		public GetOrdersListQuery(string userName)
		{
			UserName = userName;
		}

		public string UserName { get; set; }
	}
}
