using System.Net;
using AutoMapper;
using Basket.API.Src.Entities;
using Basket.API.Src.Repositories;
using EventBus.Messages.Src.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Src.Controllers
{
	[ApiController]
	[Route("api/v1/basket/[controller]")]
	[Produces("application/json")]
	public class CheckoutBasketController : ControllerBase
	{
		private readonly IBasketRepository _repository;
		private readonly IMapper _mapper;
		private readonly IPublishEndpoint _publishEndpoint;

		public CheckoutBasketController(
			IBasketRepository repository,
			IMapper mapper,
			IPublishEndpoint publishEndpoint)
		{
			this._repository = repository;
			this._mapper = mapper;
			this._publishEndpoint = publishEndpoint;
		}

		[HttpPost]
		[ProducesResponseType(typeof(BasketCheckoutEntity), (int)HttpStatusCode.Accepted)]
		[ProducesResponseType(typeof(BasketCheckoutEntity), (int)HttpStatusCode.BadRequest)]
		public async Task<ActionResult> Checkout([FromBody] BasketCheckoutEntity checkoutBasket)
		{
			BasketEntity? basket = await this._repository.GetBasket(checkoutBasket.UserName);

			if (basket == null)
			{
				return BadRequest();
			}

			BasketCheckoutEvent eventMessage = this._mapper.Map<BasketCheckoutEvent>(checkoutBasket);

			eventMessage.TotalPrice = basket.TotalPrice;

			await this._publishEndpoint.Publish(eventMessage);

			await this._repository.DeleteBasket(basket.UserName);

			return Accepted();
		}
	}
}
