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
	public class UpdateDiscountController : ControllerBase
	{
		private readonly IDiscountRepository _repository;
		private readonly ILogger<UpdateDiscountController> _logger;
		private readonly SingleDiscountResponseFactory _singleDiscountResponseFactory;

		public UpdateDiscountController(
			IDiscountRepository repository,
			ILogger<UpdateDiscountController> logger,
			SingleDiscountResponseFactory singleDiscountResponseFactory)
		{
			_repository = repository;
			_logger = logger;
			_singleDiscountResponseFactory = singleDiscountResponseFactory;
		}

		[HttpPut]
		[ProducesResponseType(typeof(DiscountDataTransferObject), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<ActionResult<DiscountDataTransferObject>> UpdateDiscount([FromBody] DiscountDataTransferObject discount)
		{
			int result;

			try
			{
				result = await _repository.UpdateDiscountAsync(discount);
			}
			catch (Exception ex)
			{
				this._logger.LogError(
					ex,
					"Unable to update discount '{Discount}'.",
					discount.ToString());

				return Problem();
			}

			if (result == 0)
			{
				_logger.LogError(
					"Unable to update discount '{Discount}'.",
					discount.ToString());

				return Problem();
			}

			ResponseDataTransferObject response = _singleDiscountResponseFactory.Create(
				discount,
				discount.ProductName);

			return Ok(response);
		}
	}
}
