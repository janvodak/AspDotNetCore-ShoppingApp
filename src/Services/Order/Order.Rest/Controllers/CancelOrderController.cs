using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Commands.CancelOrder;
using ShoppingApp.Services.Order.API.Application.Commands.Shared;
using ShoppingApp.Services.Order.API.Application.Extensions;

namespace ShoppingApp.Services.Order.API.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/Order")]
	[Produces("application/json")]
	public class CancelOrderController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<CreateOrderController> _logger;

		public CancelOrderController(
			IMediator mediator,
			ILogger<CreateOrderController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		[Route("[action]/{id:length(24)}")]
		[HttpDelete]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> CancelOrder(
			[FromHeader(Name = "x-requestid")] Guid requestId,
			int id)
		{
			if (requestId == Guid.Empty)
			{
				return BadRequest("Empty GUID is not valid for request ID");
			}

			CancelOrderCommand command = new(id);

			IdentifiedCommand<CancelOrderCommand, bool> requestCancelOrder = new(command, requestId);

			_logger.LogInformation(
				"Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
				requestCancelOrder.GetGenericTypeName(),
				nameof(requestCancelOrder.Command.Id),
				requestCancelOrder.Command.Id,
				requestCancelOrder);

			bool commandResult = await _mediator.Send(command);

			if (commandResult == false)
			{
				return BadRequest();
			}

			return NoContent();
		}
	}
}
