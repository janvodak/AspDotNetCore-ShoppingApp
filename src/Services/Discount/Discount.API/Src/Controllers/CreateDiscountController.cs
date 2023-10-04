using System.Net;
using Discount.API.Src.Entities;
using Discount.API.Src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Src.Controllers
{
	[ApiController]
	[Route("api/v1/discount/[controller]")]
	[Produces("application/json")]
	public class CreateDiscountController : ControllerBase
	{
		private readonly IDiscountRepository _repository;

		public CreateDiscountController(IDiscountRepository repository)
		{
			this._repository = repository;
		}

		[HttpPost]
		[ProducesResponseType(typeof(DiscountEntity), (int)HttpStatusCode.Created)]
		public async Task<ActionResult<DiscountEntity>> CreateDiscount([FromBody] DiscountEntity discount)
		{
			await this._repository.CreateDiscount(discount);

			return CreatedAtRoute("GetDiscount", new { productName = discount.ProductName }, discount);
		}
	}
}
