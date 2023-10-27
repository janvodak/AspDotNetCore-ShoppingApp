using System.Net;
using Basket.API.Src.Entities;
using Basket.API.Src.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Src.Controllers
{
	[ApiController]
	[Route("api/v1/basket/[controller]")]
	[Produces("application/json")]
	public class CheckoutBasketController : ControllerBase
	{
		private readonly IBasketCheckoutEventPublisher basketCheckoutEventPublisher;

		public CheckoutBasketController(IBasketCheckoutEventPublisher basketCheckoutEventPublisher)
		{
			this.basketCheckoutEventPublisher = basketCheckoutEventPublisher;
		}

		[HttpPost]
		[ProducesResponseType(typeof(BasketCheckoutEntity), (int)HttpStatusCode.Accepted)]
		[ProducesResponseType(typeof(BasketCheckoutEntity), (int)HttpStatusCode.BadRequest)]
		public async Task<ActionResult> Checkout([FromBody] BasketCheckoutEntity checkoutBasket)
		{
			try
			{
				await this.basketCheckoutEventPublisher.Publish(checkoutBasket);
			}
			catch (ArgumentNullException)
			{
				return BadRequest();
			}

			return Accepted();
		}
	}
}
