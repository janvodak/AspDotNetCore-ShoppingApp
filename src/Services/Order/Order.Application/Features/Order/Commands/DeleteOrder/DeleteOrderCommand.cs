using MediatR;

namespace ShoppingApp.Services.Order.API.Application.Features.Order.Commands.DeleteOrder
{
	public class DeleteOrderCommand : IRequest
	{
		public int Id { get; set; }
	}
}
