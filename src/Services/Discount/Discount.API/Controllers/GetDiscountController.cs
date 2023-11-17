using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Discount.API.Models;
using ShoppingApp.Services.Discount.API.Repositories;

namespace ShoppingApp.Services.Discount.API.Controllers
{
	[ApiController]
	[Route("api/v1/discount/[controller]")]
	[Produces("application/json")]
	public class GetDiscountController : ControllerBase
	{
		private readonly IDiscountRepository _repository;

		public GetDiscountController(IDiscountRepository repository)
		{
			_repository = repository;
		}

		[HttpGet("{productName}", Name = "GetDiscount")]
		[ProducesResponseType(typeof(DiscountModel), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<ActionResult<DiscountModel>> GetDiscount(string productName)
		{
			DiscountModel? discount = await _repository.GetDiscount(productName);


			if (discount == null)
			{
				return NotFound();
			}

			return Ok(discount);
		}
	}
}
