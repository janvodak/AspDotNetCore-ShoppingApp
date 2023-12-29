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

			bool result = await _mediator.Send(command);

			if (result == true)
			{
				_logger.LogInformation(
					"BasketCheckoutEvent was consumed successfully. Order command '{@Order}' was successfully processed.",
					command);
			}
			else
			{
				_logger.LogWarning(
					"There was an error during BasketCheckoutEvent was consumtion. Order command '{@Order}'.",
					command);
			}
		}
	}
}
