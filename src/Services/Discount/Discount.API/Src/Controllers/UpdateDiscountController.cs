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

		[HttpPut]
		[ProducesResponseType(typeof(DiscountEntity), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<ActionResult<DiscountEntity>> UpdateDiscount([FromBody] DiscountEntity discount)
		{
			bool result = await this._repository.UpdateDiscount(discount);

			if (result == false)
			{
				return Problem();
			}

			return Ok(discount);
		}
	}
}
