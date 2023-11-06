using System.Net;
using Basket.API.Src.Entities;
using Basket.API.Src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Src.Controllers
{
	[ApiController]
	[Route("api/v1/basket/[controller]")]
	[Produces("application/json")]
	public class DeleteBasketController : ControllerBase
	{
		private readonly IBasketRepository _repository;

		public DeleteBasketController(IBasketRepository repository)
		{
			this._repository = repository;
		}

		[HttpDelete("{userName}")]
		[ProducesResponseType(typeof(BasketEntity), (int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> DeleteBasket(string userName)
		{
			await this._repository.DeleteBasket(userName);

			return NoContent();
		}
	}
}
