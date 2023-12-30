using System.Runtime.Serialization;
using MediatR;

namespace ShoppingApp.Services.Order.API.Application.Commands.CancelOrder
{
	public class CancelOrderCommand : IRequest<bool>
	{
		public CancelOrderCommand(int id)
		{
			Id = id;
		}

		[DataMember]
		public int Id { get; private set; }
	}
}
