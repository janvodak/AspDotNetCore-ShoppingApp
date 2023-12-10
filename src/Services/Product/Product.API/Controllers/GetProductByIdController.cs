using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Product.API.Models;
using ShoppingApp.Services.Product.API.Repositories;

namespace ShoppingApp.Services.Product.API.Controllers
{
	[ApiController]
	[Route("api/v1/Product/[controller]")]
	[Produces("application/json")]
	public class GetProductByIdController : ControllerBase
	{
		private readonly IProductRepository _repository;
		private readonly ILogger<GetProductByIdController> _logger;

		public GetProductByIdController(
			IProductRepository repository,
			ILogger<GetProductByIdController> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[HttpGet("{id:length(24)}", Name = "GetProductById")]
		[ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<ActionResult<ProductModel>> GetProductById(string id)
		{
			ProductModel product = await _repository.GetProductById(id);

			if (product == null)
			{
				string message = $"Product with id: {id} not found";
				_logger.LogError(message: message);

				return NotFound();
			}

			return Ok(product);
		}

	}
}
