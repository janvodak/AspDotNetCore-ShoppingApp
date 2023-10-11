using System.Net;
using Basket.API.Src.Entities;
using Basket.API.Src.GrpcServices;
using Basket.API.Src.Repositories;
using Discount.Grpc.Src.Protos;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Src.Controllers
{
	[ApiController]
	[Route("api/v1/basket/[controller]")]
	[Produces("application/json")]
	public class UpdateBasketController : ControllerBase
	{
		private readonly IBasketRepository _repository;
		private readonly GetDiscountGrpcService _getDiscountGrpcService;

		public UpdateBasketController(IBasketRepository repository, GetDiscountGrpcService getDiscountGrpcService)
		{
			this._repository = repository;
			this._getDiscountGrpcService = getDiscountGrpcService;
		}

		[HttpPost]
		[ProducesResponseType(typeof(BasketEntity), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<BasketEntity>> UpdateBasket([FromBody] BasketEntity basket)
		{
			foreach (var product in basket.Products)
			{
				GetDiscountProtocolBufferEntity discount = await this._getDiscountGrpcService.GetDiscount(product.Name);

				product.Price -= discount.Amount;
			}

			await this._repository.UpdateBasket(basket);

			return Ok(basket);
		}
	}
}
