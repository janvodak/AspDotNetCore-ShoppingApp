using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Product.API.Models;
using ShoppingApp.Services.Product.API.Repositories;

namespace ShoppingApp.Services.Product.API.Controllers
{
	[ApiController]
	[Route("api/v1/Product/[controller]")]
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
		[ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> UpdateProduct([FromBody] ProductModel product)
		{
			bool result = await _repository.UpdateProduct(product);

			if (result == false)
			{
				string message = $"Product with id: {product.Id} not found";
				_logger.LogError(message: message);

				return NotFound();
			}

			return Ok(product);
		}
	}
}
