using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Commands.CancelOrder;
using ShoppingApp.Services.Order.API.Application.Commands.Shared;
using ShoppingApp.Services.Order.API.Application.Extensions;
using ShoppingApp.Services.Order.API.Rest.Models.DataTransferObjects;

namespace ShoppingApp.Services.Order.API.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/Order")]
	[Produces("application/json")]
	public class CancelOrderController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<CancelOrderController> _logger;

		public CancelOrderController(
			IMediator mediator,
			ILogger<CancelOrderController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		[Route("[action]")]
		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> CancelOrder(
			[FromHeader(Name = "X-Request-ID")] Guid requestId,
			[FromBody] CancelOrderCommand command)
		{
			if (requestId == Guid.Empty)
			{
				ResponseDataTransferObject response = new(
					false,
					"Empty GUID is not valid for request ID.");

				return BadRequest(response);
			}

			IdentifiedCommand<CancelOrderCommand, bool> requestCancelOrder = new(command, requestId);

			_logger.LogInformation(
				"Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
				requestCancelOrder.GetGenericTypeName(),
				nameof(requestCancelOrder.Command.Id),
				requestCancelOrder.Command.Id,
				requestCancelOrder);

			bool commandResult;

			try
			{
				commandResult = await _mediator.Send(requestCancelOrder);
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"Handling command error: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
					requestCancelOrder.GetGenericTypeName(),
					nameof(requestCancelOrder.Command.Id),
					requestCancelOrder.Command.Id,
					requestCancelOrder);

				ResponseDataTransferObject response = new(
					false,
					"There was a problem processing the request.");

				return BadRequest(response);
			}

			if (commandResult == false)
			{
				ResponseDataTransferObject response = new(
					false,
					"There was a problem processing the request.");

				return BadRequest(response);
			}

			return Ok(new ResponseDataTransferObject());
		}
	}
}
