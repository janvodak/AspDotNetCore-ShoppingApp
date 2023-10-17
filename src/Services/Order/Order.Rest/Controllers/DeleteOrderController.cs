using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Src.Features.Order.Commands.DeleteOrder;

namespace Order.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/order/[controller]")]
	[Produces("application/json")]
	public class DeleteOrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public DeleteOrderController(IMediator mediator)
		{
			this._mediator = mediator;
		}

		[HttpDelete("{id}", Name = "DeleteOrder")]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> DeleteOrder(int id)
		{
			DeleteOrderCommand command = new() { Id = id };
			await this._mediator.Send(command);

			return NoContent();
		}
	}
}
