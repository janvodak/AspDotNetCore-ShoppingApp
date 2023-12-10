using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Product.API.Models.DataTransferObjects;
using ShoppingApp.Services.Product.API.Repositories;

namespace ShoppingApp.Services.Product.API.Controllers
{
	[ApiController]
	[Route("api/v1/Product")]
	[Produces("application/json")]
	public class UpdateProductController : ControllerBase
	{
		private readonly IProductRepository _repository;
		private readonly ILogger<UpdateProductController> _logger;

		public UpdateProductController(
			IProductRepository repository,
			ILogger<UpdateProductController> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[HttpPut]
		[ProducesResponseType(typeof(ResponseDataTransferObject), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> UpdateProduct([FromBody] ProductDataTransferObject product)
		{
			bool result = await _repository.UpdateProductAsync(product);

			if (result == false)
			{
				_logger.LogError("Product with id '{Id}' was not found.", product.Id);

				ResponseDataTransferObject errorResponse = new(
					false,
					$"Product with id '{product.Id}' was not found.");

				return BadRequest(errorResponse);
			}

			ResponseDataTransferObject response = new()
			{
				Result = product
			};

			return Ok(response);
		}
	}
}
