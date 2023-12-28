using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Commands.CheckoutOrder;

namespace ShoppingApp.Services.Order.API.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/Order")]
	[Produces("application/json")]
	public class CreateOrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CreateOrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Route("[action]")]
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
		{
			bool result = await _mediator.Send(command);

			return result ? NoContent() : BadRequest();
		}
	}
}
