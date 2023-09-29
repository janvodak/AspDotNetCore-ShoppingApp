using System.Net;
using Catalog.API.Src.Entities;
using Catalog.API.Src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[ApiController]
	[Route("api/v1/catalog/[controller]")]
	[Produces("application/json")]
	public class GetProductsByCategoryController : ControllerBase
	{
		private readonly IProductRepository _repository;

		public GetProductsByCategoryController(IProductRepository repository)
		{
			this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
		{
			IEnumerable<Product> product = await this._repository.GetProductsByCategory(category);

			return Ok(product);
		}
	}
}
