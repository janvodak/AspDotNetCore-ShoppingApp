using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Discount.API.Models;
using ShoppingApp.Services.Discount.API.Repositories;

namespace ShoppingApp.Services.Discount.API.Controllers
{
	[ApiController]
	[Route("api/v1/discount/[controller]")]
	[Produces("application/json")]
	public class DeleteDiscountController : ControllerBase
	{
		private readonly IDiscountRepository _repository;

		public DeleteDiscountController(IDiscountRepository repository)
		{
			_repository = repository;
		}

		[HttpDelete("{productName}")]
		[ProducesResponseType(typeof(DiscountModel), (int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> DeleteDiscount(string productName)
		{
			bool result = await _repository.DeleteDiscount(productName);

			if (result == false)
			{
				return Problem();
			}

			return NoContent();
		}
	}
}
