using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Features.Order.Commands.DeleteOrder;

namespace ShoppingApp.Services.Order.API.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/order/[controller]")]
	[Produces("application/json")]
	public class DeleteOrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public DeleteOrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpDelete("{id}")]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> DeleteOrder(int id)
		{
			DeleteOrderCommand command = new() { Id = id };
			await _mediator.Send(command);

			return NoContent();
		}
	}
}
