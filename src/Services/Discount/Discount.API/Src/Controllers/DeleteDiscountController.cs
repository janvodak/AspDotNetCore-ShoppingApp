using System.Net;
using Discount.API.Src.Entities;
using Discount.API.Src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Src.Controllers
{
	[ApiController]
	[Route("api/v1/discount/[controller]")]
	[Produces("application/json")]
	public class DeleteDiscountController : ControllerBase
	{
		private readonly IDiscountRepository _repository;

		public DeleteDiscountController(IDiscountRepository repository)
		{
			this._repository = repository;
		}

		[HttpDelete("{productName}", Name = "DeleteDiscount")]
		[ProducesResponseType(typeof(DiscountEntity), (int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> DeleteDiscount(string productName)
		{
			await this._repository.DeleteDiscount(productName);

			return NoContent();
		}
	}
}
