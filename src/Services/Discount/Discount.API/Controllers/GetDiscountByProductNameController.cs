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
	public class GetDiscountByProductNameController : ControllerBase
	{
		private readonly IDiscountRepository _repository;
		private readonly ILogger<GetDiscountByProductNameController> _logger;
		private readonly SingleDiscountResponseFactory _responseFactory;

		public GetDiscountByProductNameController(
			IDiscountRepository repository,
			ILogger<GetDiscountByProductNameController> logger,
			SingleDiscountResponseFactory responseFactory)
		{
			_repository = repository;
			_logger = logger;
			_responseFactory = responseFactory;
		}

		[HttpGet("{productName}", Name = "GetDiscount")]
		[ProducesResponseType(typeof(DiscountDataTransferObject), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<ActionResult<ResponseDataTransferObject>> GetDiscountByProductNameAsync(string productName)
		{
			DiscountDataTransferObject? discount;

			try
			{
				discount = await _repository.GetDiscountByProductNameAsync(productName);
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"Unable to get discount for product '{ProductName}'",
					productName);

				return Problem();
			}

			ResponseDataTransferObject response = _responseFactory.Create(
				discount,
				productName);

			return Ok(response);
		}
	}
}
