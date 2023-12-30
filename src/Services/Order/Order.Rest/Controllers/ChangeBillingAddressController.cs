using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Commands.ChangeBillingAddress;
using ShoppingApp.Services.Order.API.Application.Commands.Shared;
using ShoppingApp.Services.Order.API.Application.Extensions;

namespace ShoppingApp.Services.Order.API.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/Order")]
	[Produces("application/json")]
	public class ChangeBillingAddressController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<CreateOrderController> _logger;

		public ChangeBillingAddressController(
			IMediator mediator,
			ILogger<CreateOrderController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		[Route("[action]")]
		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> ChangeBillingAddress(
			[FromHeader(Name = "x-requestid")] Guid requestId,
			[FromBody] ChangeBillingAddressCommand command)
		{
			if (requestId == Guid.Empty)
			{
				return BadRequest("Empty GUID is not valid for request ID");
			}

			IdentifiedCommand<ChangeBillingAddressCommand, bool> requestChangeBillingAddress = new(command, requestId);

			_logger.LogInformation(
				"Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
				requestChangeBillingAddress.GetGenericTypeName(),
				nameof(requestChangeBillingAddress.Command.Id),
				requestChangeBillingAddress.Command.Id,
				requestChangeBillingAddress);

			bool commandResult = await _mediator.Send(command);

			if (commandResult == false)
			{
				return BadRequest();
			}

			return NoContent();
		}
	}
}
