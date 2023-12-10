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
	public class DeleteDiscountController : ControllerBase
	{
		private readonly IDiscountRepository _repository;
		private readonly ILogger<DeleteDiscountController> _logger;
		private readonly SingleDiscountResponseFactory _singleDiscountResponseFactory;

		public DeleteDiscountController(
			IDiscountRepository repository,
			ILogger<DeleteDiscountController> logger,
			SingleDiscountResponseFactory singleDiscountResponseFactory)
		{
			_repository = repository;
			_logger = logger;
			_singleDiscountResponseFactory = singleDiscountResponseFactory;
		}

		[HttpDelete("{productName}")]
		[ProducesResponseType(typeof(DiscountDataTransferObject), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> DeleteDiscount(string productName)
		{
			int result;

			try
			{
				result = await _repository.DeleteDiscountAsync(productName);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Unable to remove discount for product '{ProductName}'",
					productName);

				return Problem();
			}

			if (result == 0)
			{
				_logger.LogError("Unable to remove discount for product '{ProductName}'",
					productName);

				return Problem();
			}

			ResponseDataTransferObject response = _singleDiscountResponseFactory.Create(
				null,
				productName);

			return Ok(response);
		}
	}
}
