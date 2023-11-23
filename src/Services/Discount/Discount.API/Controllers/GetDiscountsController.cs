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
	public class GetDiscountsController : ControllerBase
	{
		private readonly IDiscountRepository _repository;
		private readonly ILogger<GetDiscountsController> _logger;
		private readonly MultipleDiscountsResponseFactory _responseFactory;

		public GetDiscountsController(
			IDiscountRepository repository,
			ILogger<GetDiscountsController> logger,
			MultipleDiscountsResponseFactory responseFactory)
		{
			_repository = repository;
			_logger = logger;
			_responseFactory = responseFactory;
		}

		[HttpGet]
		[ProducesResponseType(typeof(DiscountDataTransferObject), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<ActionResult<ResponseDataTransferObject>> GetDiscountsAsync()
		{
			IEnumerable<DiscountDataTransferObject> discountDataTransferObjects;

			try
			{
				discountDataTransferObjects = await _repository.GetDiscountsAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"Unable to get discounts.");

				return Problem();
			}

			ResponseDataTransferObject response = _responseFactory.CreateEnumerableDiscount(discountDataTransferObjects);

			return Ok(response);
		}
	}
}
