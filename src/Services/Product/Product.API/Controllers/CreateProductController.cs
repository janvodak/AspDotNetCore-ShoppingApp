using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Product.API.Models.DataTransferObjects;
using ShoppingApp.Services.Product.API.Repositories;

namespace ShoppingApp.Services.Product.API.Controllers
{
	[ApiController]
	[Route("api/v1/Product")]
	[Produces("application/json")]
	public class CreateProductController : ControllerBase
	{
		private readonly IProductRepository _repository;

		public CreateProductController(IProductRepository repository)
		{
			_repository = repository;
		}

		[HttpPost]
		[ProducesResponseType(typeof(ResponseDataTransferObject), (int)HttpStatusCode.Created)]
		public async Task<ActionResult<ProductDataTransferObject>> CreateProduct([FromBody] ProductDataTransferObject product)
		{
			await _repository.CreateProductAsync(product);

			ResponseDataTransferObject response = new()
			{
				Result = product
			};

			return Created("GetProductById", response);
		}
	}
}
