using System.Runtime.Serialization;
using MediatR;

namespace ShoppingApp.Services.Order.API.Application.Commands.DeleteOrder
{
	[DataContract]
	public class DeleteOrderCommand : IRequest<bool>
	{
		[DataMember]
		public int Id { get; private set; }

		public DeleteOrderCommand(int id)
		{
			Id = id;
		}
	}
}
