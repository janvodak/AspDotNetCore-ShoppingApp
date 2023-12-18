using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Features.Order.Commands.CheckoutOrder;

namespace ShoppingApp.Services.Order.API.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/order/[controller]")]
	[Produces("application/json")]
	public class CheckoutOrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CheckoutOrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
		{
			int result = await _mediator.Send(command);

			return Ok(result);
		}
	}
}
