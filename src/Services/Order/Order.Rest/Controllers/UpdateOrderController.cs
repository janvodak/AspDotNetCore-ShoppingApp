using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Src.Features.Order.Commands.UpdateOrder;

namespace Order.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/order/[controller]")]
	[Produces("application/json")]
	public class UpdateOrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UpdateOrderController(IMediator mediator)
		{
			this._mediator = mediator;
		}

		[HttpPut(Name = "UpdateOrder")]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
		{
			await this._mediator.Send(command);

			return NoContent();
		}
	}
}

