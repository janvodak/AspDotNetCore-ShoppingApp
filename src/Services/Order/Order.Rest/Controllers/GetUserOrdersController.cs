using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Queries.GetOrdersList;
using ShoppingApp.Services.Order.API.Rest.Models.DataTransferObjects;

namespace ShoppingApp.Services.Order.API.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/Order")]
	[Produces("application/json")]
	public class GetUserOrdersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public GetUserOrdersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{userName}")]
		[ProducesResponseType(typeof(IEnumerable<OrderDataTransferObject>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<OrderDataTransferObject>>> GetUserOrders(string userName)
		{
			GetOrdersListQuery query = new(userName);

			List<OrderDataTransferObject> orders = await _mediator.Send(query);

			return Ok(new ResponseDataTransferObject(orders));
		}
	}
}
