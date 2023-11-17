using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Discount.API.Models;
using ShoppingApp.Services.Discount.API.Repositories;

namespace ShoppingApp.Services.Discount.API.Controllers
{
	[ApiController]
	[Route("api/v1/discount/[controller]")]
	[Produces("application/json")]
	public class CreateDiscountController : ControllerBase
	{
		private readonly IDiscountRepository _repository;

		public CreateDiscountController(IDiscountRepository repository)
		{
			_repository = repository;
		}

		[HttpPost]
		[ProducesResponseType(typeof(DiscountModel), (int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<ActionResult<DiscountModel>> CreateDiscount([FromBody] DiscountModel discount)
		{
			bool result = await _repository.CreateDiscount(discount);

			if (result == false)
			{
				return Problem();
			}

			return CreatedAtRoute("GetDiscount", new { productName = discount.ProductName }, discount);
		}
	}
}
