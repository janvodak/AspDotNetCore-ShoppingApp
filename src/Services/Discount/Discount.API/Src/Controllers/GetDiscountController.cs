using System.Net;
using Discount.API.Src.Entities;
using Discount.API.Src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Src.Controllers
{
	[ApiController]
	[Route("api/v1/discount/[controller]")]
	[Produces("application/json")]
	public class GetDiscountController : ControllerBase
	{
		private readonly IDiscountRepository _repository;

		public GetDiscountController(IDiscountRepository repository)
		{
			this._repository = repository;
		}

		[HttpGet("{productName}", Name = "GetDiscount")]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(DiscountEntity), (int)HttpStatusCode.Accepted)]
		public async Task<ActionResult<DiscountEntity>> GetDiscount(string productName)
		{
			DiscountEntity? discount = await this._repository.GetDiscount(productName);


			if (discount == null)
			{
				return NotFound();
			}

			return Accepted(discount);
		}
	}
}
