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

		public DeleteProductController(IProductRepository repository)
		{
			this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		[HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
		[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> DeleteProductById(string id)
		{
			await this._repository.DeleteProduct(id);

			return Ok();
		}
	}
}
