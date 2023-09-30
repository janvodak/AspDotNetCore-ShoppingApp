using System.Net;
using Catalog.API.Src.Entities;
using Catalog.API.Src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[ApiController]
	[Route("api/v1/catalog/[controller]")]
	[Produces("application/json")]
	public class UpdateProductController : ControllerBase
	{
		private readonly IProductRepository _repository;
		private readonly ILogger<GetProductByIdController> _logger;

		public UpdateProductController(IProductRepository repository, ILogger<GetProductByIdController> logger)
		{
			this._repository = repository;
			this._logger = logger;
		}

		[HttpPut]
		[ProducesResponseType(typeof(Product), (int)HttpStatusCode.NoContent)]
		public async Task<IActionResult> UpdateProduct([FromBody] Product product)
		{
			bool result = await this._repository.UpdateProduct(product);

			if (result == false)
			{
				string message = $"Product with id: {product.Id} not found";
				this._logger.LogError(message: message);

				return NotFound();
			}

			return NoContent();
		}
	}
}
