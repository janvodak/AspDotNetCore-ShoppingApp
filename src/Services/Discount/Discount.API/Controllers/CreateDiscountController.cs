using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Discount.API.Models.DataTransferObjects;
using ShoppingApp.Services.Discount.API.Models.DataTransferObjects.Factories;
using ShoppingApp.Services.Discount.API.Repositories;

namespace ShoppingApp.Services.Discount.API.Controllers
{
	[ApiController]
	[Route("api/v1/Discount")]
	[Produces("application/json")]
	public class CreateDiscountController : ControllerBase
	{
		private readonly IDiscountRepository _repository;
		private readonly ILogger<CreateDiscountController> _logger;
		private readonly SingleDiscountResponseFactory _responseFactory;

		public CreateDiscountController(
			IDiscountRepository repository,
			ILogger<CreateDiscountController> logger,
			SingleDiscountResponseFactory responseFactory)
		{
			_repository = repository;
			_logger = logger;
			_responseFactory = responseFactory;
		}

		[HttpPost]
		[ProducesResponseType(typeof(DiscountDataTransferObject), (int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<ActionResult<DiscountDataTransferObject>> CreateDiscount([FromBody] DiscountDataTransferObject discount)
		{
			int result = await _repository.CreateDiscountAsync(discount);

			if (result == 0)
			{
				_logger.LogError(
					"Unable to create discount: '{Discount}'",
					discount.ToString());

				return Problem();
			}

			DiscountDataTransferObject? discountDataTransferObject = await _repository.GetDiscountByProductNameAsync(discount.ProductName);

			if (discountDataTransferObject == null)
			{
				return Problem();
			}

			ResponseDataTransferObject response = _responseFactory.Create(
				discountDataTransferObject,
				discountDataTransferObject.ProductName);

			return Created("GetDiscount", response);
		}
	}
}
