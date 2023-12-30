using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Commands.CreateOrder;
using ShoppingApp.Services.Order.API.Application.Commands.Shared;
using ShoppingApp.Services.Order.API.Application.Extensions;

namespace ShoppingApp.Services.Order.API.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/Order")]
	[Produces("application/json")]
	public class CreateOrderController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<CreateOrderController> _logger;

		public CreateOrderController(
			IMediator mediator,
			ILogger<CreateOrderController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		[Route("[action]")]
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<ActionResult<int>> CreateOrder(
			[FromHeader(Name = "x-requestid")] Guid requestId,
			[FromBody] CreateOrderCommand command)
		{
			if (requestId == Guid.Empty)
			{
				return BadRequest("Empty GUID is not valid for request ID");
			}

			IdentifiedCommand<CreateOrderCommand, bool> requestCreateOrder = new(command, requestId);

			_logger.LogInformation(
				"Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
				requestCreateOrder.GetGenericTypeName(),
				nameof(requestCreateOrder.Command.EmailAddress),
				requestCreateOrder.Command.EmailAddress,
				requestCreateOrder);

			bool commandResult = await _mediator.Send(requestCreateOrder);

			if (commandResult == false)
			{
				return BadRequest();
			}

			return NoContent();
		}
	}
}
