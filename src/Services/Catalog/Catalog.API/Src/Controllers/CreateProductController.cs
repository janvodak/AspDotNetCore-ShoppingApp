using System.Net;
using Catalog.API.Src.Entities;
using Catalog.API.Src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[ApiController]
	[Route("api/v1/catalog/[controller]")]
	[Produces("application/json")]
	public class CreateProductController : ControllerBase
	{
		private readonly IProductRepository _repository;

		public CreateProductController(IProductRepository repository)
		{
			this._repository = repository;
		}

		[HttpPost]
		[ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
		public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
		{
			await this._repository.CreateProduct(product);

			return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
		}
	}
}
