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
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType(typeof(DiscountEntity), (int)HttpStatusCode.Accepted)]
		public async Task<IActionResult> DeleteDiscount(string productName)
		{
			bool result = await this._repository.DeleteDiscount(productName);

			if (result == false)
			{
				return Problem();
			}

			return Accepted();
		}
	}
}
