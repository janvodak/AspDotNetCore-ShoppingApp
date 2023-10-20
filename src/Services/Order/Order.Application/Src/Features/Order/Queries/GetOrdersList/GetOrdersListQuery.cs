using MediatR;

namespace Order.Application.Src.Features.Order.Queries.GetOrdersList
{
	public class GetOrdersListQuery : IRequest<List<OrderDataTransferObject>>
	{
		public string UserName { get; set; }

		public GetOrdersListQuery(string userName)
		{
			UserName = userName;
		}
	}
}
