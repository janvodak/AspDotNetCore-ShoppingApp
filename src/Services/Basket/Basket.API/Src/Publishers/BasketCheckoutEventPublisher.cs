using AutoMapper;
using Basket.API.Src.Entities;
using Basket.API.Src.Repositories;
using MassTransit;
using ShoppingApp.Components.EventBus.Messages.Events;

namespace Basket.API.Src.Publishers
{
	public class BasketCheckoutEventPublisher : IBasketCheckoutEventPublisher
	{
		private readonly IBasketRepository _repository;
		private readonly IMapper _mapper;
		private readonly IPublishEndpoint _publishEndpoint;

		public BasketCheckoutEventPublisher(
			IBasketRepository repository,
			IMapper mapper,
			IPublishEndpoint publishEndpoint)
		{
			this._repository = repository;
			this._mapper = mapper;
			this._publishEndpoint = publishEndpoint;
		}

		public async Task Publish(BasketCheckoutEntity checkoutBasket)
		{
			BasketEntity? basket = await this._repository.GetBasket(checkoutBasket.UserName);

			if (basket == null)
			{
				throw new NullReferenceException();
			}

			BasketCheckoutEvent eventMessage = this._mapper.Map<BasketCheckoutEvent>(checkoutBasket);

			eventMessage.TotalPrice = basket.TotalPrice;

			await this._publishEndpoint.Publish(eventMessage);

			await this._repository.DeleteBasket(basket.UserName);
		}
	}
}
