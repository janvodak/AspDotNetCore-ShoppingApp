using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Src.Features.Order.Commands.CheckoutOrder;

namespace Order.Rest.Src.Controllers
{
	[ApiController]
	[Route("api/v1/order/[controller]")]
	[Produces("application/json")]
	public class CheckoutOrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CheckoutOrderController(IMediator mediator)
		{
			this._mediator = mediator;
		}

		[HttpPost(Name = "CheckoutOrder")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
		{
			int result = await this._mediator.Send(command);

			return Ok(result);
		}
	}
}
