using System.Net;
using Catalog.API.Src.Entities;
using Catalog.API.Src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[ApiController]
	[Route("api/v1/catalog/[controller]")]
	[Produces("application/json")]
	public class DeleteProductController : ControllerBase
	{
		private readonly IProductRepository _repository;
		private readonly ILogger<GetProductByIdController> _logger;

		public DeleteProductController(IProductRepository repository, ILogger<GetProductByIdController> logger)
		{
			this._repository = repository;
			this._logger = logger;
		}

		[HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
		[ProducesResponseType(typeof(Product), (int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> DeleteProductById(string id)
		{
			bool result = await this._repository.DeleteProduct(id);

			if (result == false)
			{
				string message = $"Product with id: {id} not found";
				this._logger.LogError(message: message);

				return NotFound();
			}

			return NoContent();
		}
	}
}
