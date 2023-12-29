using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Commands.DeleteOrder;
using static MassTransit.ValidationResultExtensions;

namespace ShoppingApp.Services.Order.API.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/Order")]
	[Produces("application/json")]
	public class CancelOrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CancelOrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Route("[action]/{id:length(24)}")]
		[HttpDelete]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> CancelOrder(int id)
		{
			CancelOrderCommand command = new(id);

			bool result = await _mediator.Send(command);

			return result ? NoContent() : BadRequest();
		}
	}
}
