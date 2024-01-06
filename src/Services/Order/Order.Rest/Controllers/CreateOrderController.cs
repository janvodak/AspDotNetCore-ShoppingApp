using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Commands.CreateOrder;
using ShoppingApp.Services.Order.API.Application.Commands.Shared;
using ShoppingApp.Services.Order.API.Application.Extensions;
using ShoppingApp.Services.Order.API.Rest.Models.DataTransferObjects;

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
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<ActionResult<int>> CreateOrder(
			[FromHeader(Name = "X-Request-ID")] Guid requestId,
			[FromBody] CreateOrderCommand command)
		{
			if (requestId == Guid.Empty)
			{
				ResponseDataTransferObject response = new(
					false,
					"Empty GUID is not valid for request ID.");

				return BadRequest(response);
			}

			IdentifiedCommand<CreateOrderCommand, bool> requestCreateOrder = new(command, requestId);

			_logger.LogInformation(
				"Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
				requestCreateOrder.GetGenericTypeName(),
				nameof(requestCreateOrder.Command.EmailAddress),
				requestCreateOrder.Command.EmailAddress,
				requestCreateOrder);

			bool commandResult;

			try
			{
				commandResult = await _mediator.Send(requestCreateOrder);
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"Handling command error: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
					requestCreateOrder.GetGenericTypeName(),
					nameof(requestCreateOrder.Command.EmailAddress),
					requestCreateOrder.Command.EmailAddress,
					requestCreateOrder);

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
