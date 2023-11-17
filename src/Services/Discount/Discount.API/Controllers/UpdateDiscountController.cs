using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Discount.API.Models;
using ShoppingApp.Services.Discount.API.Repositories;

namespace ShoppingApp.Services.Discount.API.Controllers
{
	[ApiController]
	[Route("api/v1/discount/[controller]")]
	[Produces("application/json")]
	public class UpdateDiscountController : ControllerBase
	{
		private readonly IDiscountRepository _repository;

		public UpdateDiscountController(IDiscountRepository repository)
		{
			_repository = repository;
		}

		[HttpPut]
		[ProducesResponseType(typeof(DiscountModel), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<ActionResult<DiscountModel>> UpdateDiscount([FromBody] DiscountModel discount)
		{
			bool result = await _repository.UpdateDiscount(discount);

			if (result == false)
			{
				return Problem();
			}

			return Ok(discount);
		}
	}
}
