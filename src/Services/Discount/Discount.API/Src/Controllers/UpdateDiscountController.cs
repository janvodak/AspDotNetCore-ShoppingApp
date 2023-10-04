using System.Net;
using Discount.API.Src.Entities;
using Discount.API.Src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Src.Controllers
{
	[ApiController]
	[Route("api/v1/discount/[controller]")]
	[Produces("application/json")]
	public class UpdateDiscountController : ControllerBase
	{
		private readonly IDiscountRepository _repository;

		public UpdateDiscountController(IDiscountRepository repository)
		{
			this._repository = repository;
		}

		[HttpPost]
		[ProducesResponseType(typeof(DiscountEntity), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<DiscountEntity>> UpdateDiscount([FromBody] DiscountEntity discount)
		{
			await this._repository.UpdateDiscount(discount);

			return CreatedAtRoute("GetDiscount", new { productName = discount.ProductName }, discount);
		}
	}
}
