using MediatR;

namespace Order.Application.Src.Features.Order.Commands.DeleteOrder
{
	public class DeleteOrderCommand : IRequest
	{
		public int Id { get; set; }
	}
}
