using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Src.Contracts.Persistence;
using Order.Application.Src.Services;
using Order.Domain.Src.Order.Entities;

namespace Order.Application.Src.Features.Order.Commands.CheckoutOrder
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
			this._orderRepository = orderRepository;
			this._mapper = mapper;
			this._emailService = emailService;
			this._logger = logger;
		}

		public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
		{
			OrderEntity orderEntity = this._mapper.Map<OrderEntity>(request);

			OrderEntity newOrder = await this._orderRepository.AddAsync(orderEntity);

			this._logger.LogInformation($"Order '{newOrder.Id}' was successfully created.");

			await this._emailService.SendMail(newOrder);

			return newOrder.Id;
		}
	}
}