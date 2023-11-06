using System.Net;
using Basket.API.Src.Entities;
using Basket.API.Src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Src.Controllers
{
	[ApiController]
	[Route("api/v1/basket/[controller]")]
	[Produces("application/json")]
	public class GetBasketController : ControllerBase
	{
		private readonly IBasketRepository _repository;

		public GetBasketController(IBasketRepository repository)
		{
			this._repository = repository;
		}

		[HttpGet("{userName}")]
		[ProducesResponseType(typeof(BasketEntity), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<BasketEntity>> GetBasket(string userName)
		{
			BasketEntity? basket = await this._repository.GetBasket(userName);

			if (basket == null)
			{
				basket = new BasketEntity(userName);
			}

			return Ok(basket);
		}

	}
}
