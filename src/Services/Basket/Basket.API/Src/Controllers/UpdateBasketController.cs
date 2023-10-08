using System.Net;
using Basket.API.Src.Entities;
using Basket.API.Src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Src.Controllers
{
	[ApiController]
	[Route("api/v1/basket/[controller]")]
	[Produces("application/json")]
	public class UpdateBasketController : ControllerBase
	{
		private readonly IBasketRepository _repository;

		public UpdateBasketController(IBasketRepository repository)
		{
			this._repository = repository;
		}

		[HttpPost]
		[ProducesResponseType(typeof(BasketEntity), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<BasketEntity>> UpdateBasket([FromBody] BasketEntity basket)
		{
			await this._repository.UpdateBasket(basket);

			return Ok(basket);
		}
	}
}
