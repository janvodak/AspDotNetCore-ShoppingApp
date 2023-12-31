﻿using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Order.API.Application.Commands.ChangeBillingAddress;
using ShoppingApp.Services.Order.API.Application.Commands.Shared;
using ShoppingApp.Services.Order.API.Application.Extensions;
using ShoppingApp.Services.Order.API.Rest.Models.DataTransferObjects;

namespace ShoppingApp.Services.Order.API.Rest.Controllers
{
	[ApiController]
	[Route("api/v1/Order")]
	[Produces("application/json")]
	public class ChangeBillingAddressController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<ChangeBillingAddressController> _logger;

		public ChangeBillingAddressController(
			IMediator mediator,
			ILogger<ChangeBillingAddressController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		[Route("[action]")]
		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> ChangeBillingAddress(
			[FromHeader(Name = "X-Request-ID")] Guid requestId,
			[FromBody] ChangeBillingAddressCommand command)
		{
			if (requestId == Guid.Empty)
			{
				ResponseDataTransferObject response = new(
					false,
					"Empty GUID is not valid for request ID.");

				return BadRequest(response);
			}

			IdentifiedCommand<ChangeBillingAddressCommand, bool> requestChangeBillingAddress = new(command, requestId);

			_logger.LogInformation(
				"Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
				requestChangeBillingAddress.GetGenericTypeName(),
				nameof(requestChangeBillingAddress.Command.Id),
				requestChangeBillingAddress.Command.Id,
				requestChangeBillingAddress);

			bool commandResult;

			try
			{
				commandResult = await _mediator.Send(requestChangeBillingAddress);
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"Handling command error: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
					requestChangeBillingAddress.GetGenericTypeName(),
					nameof(requestChangeBillingAddress.Command.Id),
					requestChangeBillingAddress.Command.Id,
					requestChangeBillingAddress);

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
