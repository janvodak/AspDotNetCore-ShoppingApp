using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingApp.Services.Order.API.Application.Contracts.Persistence;
using ShoppingApp.Services.Order.API.Application.Services;
using ShoppingApp.Services.Order.API.Domain.Order;

namespace ShoppingApp.Services.Order.API.Application.Features.Order.Commands.CheckoutOrder
{
	public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;
		private readonly CheckoutOrderEmailService _emailService;
		private readonly ILogger<CheckoutOrderCommandHandler> _logger;

		public CheckoutOrderCommandHandler(
			IOrderRepository orderRepository,
			IMapper mapper,
			CheckoutOrderEmailService emailService,
			ILogger<CheckoutOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
			_emailService = emailService;
			_logger = logger;
		}

		public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
		{
			OrderEntity orderEntity = _mapper.Map<OrderEntity>(request);

			OrderEntity newOrder = await _orderRepository.AddAsync(orderEntity);

			_logger.LogInformation(
				"Order '{OrderId}' was successfully created.",
				newOrder.Id);

			await _emailService.Send(newOrder);

			return newOrder.Id;
		}
	}
}