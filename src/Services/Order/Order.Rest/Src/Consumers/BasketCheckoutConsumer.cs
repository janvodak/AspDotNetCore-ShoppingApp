using AutoMapper;
using EventBus.Messages.Src.Events;
using MassTransit;
using MediatR;
using Order.Application.Src.Features.Order.Commands.CheckoutOrder;

namespace Order.Rest.Src.Consumers
{
	public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;
		private readonly ILogger<BasketCheckoutConsumer> _logger;

		public BasketCheckoutConsumer(
			IMapper mapper,
			IMediator mediator,
			ILogger<BasketCheckoutConsumer> logger)
		{
			this._mapper = mapper;
			this._mediator = mediator;
			this._logger = logger;
		}

		public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
		{
			CheckoutOrderCommand command = _mapper.Map<CheckoutOrderCommand>(context.Message);

			int result = await _mediator.Send(command);

			this._logger.LogInformation(
				"BasketCheckoutEvent was consumed successfully. Order with id: '{OrderId}' was successfully created.",
				result);
		}
	}
}
