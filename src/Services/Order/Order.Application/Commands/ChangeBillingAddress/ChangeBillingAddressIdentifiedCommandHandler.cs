using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Commands.Shared;
using ShoppingApp.Services.Order.API.Application.Contracts.Idempotency;

namespace ShoppingApp.Services.Order.API.Application.Commands.ChangeBillingAddress
{
	public class ChangeBillingAddressIdentifiedCommandHandler : IdentifiedCommandHandler<ChangeBillingAddressCommand, bool>
	{
		public ChangeBillingAddressIdentifiedCommandHandler(
			IMediator mediator,
			IRequestManager requestManager,
			ILogger<IdentifiedCommandHandler<ChangeBillingAddressCommand, bool>> logger)
			: base(mediator, requestManager, logger)
		{
		}

		protected override bool CreateResultForDuplicateRequest()
		{
			return true; // Ignore duplicate requests for processing order.
		}
	}
}
