using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Features.Order.Commands.UpdateOrder;

namespace ShoppingApp.Services.Order.API.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/order/[controller]")]
	[Produces("application/json")]
	public class UpdateOrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UpdateOrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
		{
			await _mediator.Send(command);

			return NoContent();
		}
	}
}

