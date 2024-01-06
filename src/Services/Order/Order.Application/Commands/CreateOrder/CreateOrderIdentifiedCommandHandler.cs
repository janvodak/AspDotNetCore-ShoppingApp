using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Commands.Shared;
using ShoppingApp.Services.Order.API.Application.Contracts.Idempotency;

namespace ShoppingApp.Services.Order.API.Application.Commands.CreateOrder
{
	public class CreateOrderIdentifiedCommandHandler : IdentifiedCommandHandler<CreateOrderCommand, bool>
	{
		public CreateOrderIdentifiedCommandHandler(
			IMediator mediator,
			IRequestManager requestManager,
			ILogger<IdentifiedCommandHandler<CreateOrderCommand, bool>> logger)
			: base(mediator, requestManager, logger)
		{
		}

		protected override bool CreateResultForDuplicateRequest()
		{
			return true; // Ignore duplicate requests for creating order.
		}
	}
}
