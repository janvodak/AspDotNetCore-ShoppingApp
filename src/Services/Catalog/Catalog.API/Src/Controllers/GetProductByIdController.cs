using System.Net;
using Catalog.API.Src.Entities;
using Catalog.API.Src.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[ApiController]
	[Route("api/v1/catalog/[controller]")]
	[Produces("application/json")]
	public class GetProductByIdController : ControllerBase
	{
		private readonly IProductRepository _repository;
		private readonly ILogger<GetProductByIdController> _logger;

		public GetProductByIdController(IProductRepository repository, ILogger<GetProductByIdController> logger)
		{
			this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
			this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet("{id:length(24)}", Name = "GetProductById")]
		[ProducesResponseType((int) HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<Product>> GetProductById(string id)
		{
			Product product = await this._repository.GetProductById(id);

			if (product == null)
			{
				string message = $"Product with id: {id} not found";
				this._logger.LogError(message);

				return NotFound();
			}

			return Ok(product);
		}

	}
}
