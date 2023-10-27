using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Src.Features.Order.Queries.GetOrdersList;

namespace Order.Rest.Src.Controllers
{
	[ApiController]
	[Route("api/v1/order/[controller]")]
	[Produces("application/json")]
	public class GetUserOrdersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public GetUserOrdersController(IMediator mediator)
		{
			this._mediator = mediator;
		}

		[HttpGet("{userName}", Name = "GetUserOrders")]
		[ProducesResponseType(typeof(IEnumerable<OrderDataTransferObject>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<OrderDataTransferObject>>> GetUserOrders(string userName)
		{
			GetOrdersListQuery query = new(userName);

			List<OrderDataTransferObject> orders = await this._mediator.Send(query);

			return Ok(orders);
		}
	}
}
