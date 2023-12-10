using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Product.API.Models;
using ShoppingApp.Services.Product.API.Repositories;

namespace ShoppingApp.Services.Product.API.Controllers
{
	[ApiController]
	[Route("api/v1/Product/[controller]")]
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
		[ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> DeleteProductById(string id)
		{
			bool result = await _repository.DeleteProduct(id);

			if (result == false)
			{
				string message = $"Product with id: {id} not found";
				_logger.LogError(message: message);

				return NotFound();
			}

			return NoContent();
		}
	}
}
