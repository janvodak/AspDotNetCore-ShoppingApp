using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Commands.UpdateOrder;

namespace ShoppingApp.Services.Order.API.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/Order")]
	[Produces("application/json")]
	public class ChangeBillingAddressController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ChangeBillingAddressController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Route("[action]")]
		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> ChangeBillingAddress([FromBody] ChangeBillingAddressCommand command)
		{
			bool result = await _mediator.Send(command);

			return result ? NoContent() : BadRequest();
		}
	}
}

