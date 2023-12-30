using System.Runtime.Serialization;
using MediatR;

namespace ShoppingApp.Services.Order.API.Application.Queries.GetOrdersList
{
	public class GetOrdersListQuery : IRequest<List<OrderDataTransferObject>>
	{
		public GetOrdersListQuery(string userName)
		{
			UserName = userName;
		}

		[DataMember]
		public string UserName { get; private set; }
	}
}
