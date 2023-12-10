using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Product.API.Models.DataTransferObjects;
using ShoppingApp.Services.Product.API.Repositories;

namespace ShoppingApp.Services.Product.API.Controllers
{
	[ApiController]
	[Route("api/v1/Product")]
	[Produces("application/json")]
	public class DeleteProductController : ControllerBase
	{
		private readonly IProductRepository _repository;
		private readonly ILogger<DeleteProductController> _logger;

		public DeleteProductController(
			IProductRepository repository,
			ILogger<DeleteProductController> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
		[ProducesResponseType(typeof(ResponseDataTransferObject), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> DeleteProductById(string id)
		{
			bool result = await _repository.DeleteProductAsync(id);

			if (result == false)
			{
				_logger.LogError("Product with id '{Id}' was not found.", id);

				ResponseDataTransferObject response = new(
					false,
					$"Product with id '{id}' was not found.");

				return BadRequest(response);
			}

			return Ok(new ResponseDataTransferObject());
		}
	}
}
