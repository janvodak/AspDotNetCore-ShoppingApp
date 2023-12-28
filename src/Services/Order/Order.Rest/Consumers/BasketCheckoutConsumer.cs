using AutoMapper;
using EventBus.Messages.Src.Events;
using MassTransit;
using MediatR;
using ShoppingApp.Services.Order.API.Application.Commands.CheckoutOrder;

namespace ShoppingApp.Services.Order.API.Rest.Consumers
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
			_mapper = mapper;
			_mediator = mediator;
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
		{
			CreateOrderCommand command = _mapper.Map<CreateOrderCommand>(context.Message);

			int result = await _mediator.Send(command);

			_logger.LogInformation(
				"BasketCheckoutEvent was consumed successfully. Order with id: '{OrderId}' was successfully created.",
				result);
		}
	}
}
