using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Product.API.Models;
using ShoppingApp.Services.Product.API.Repositories;

namespace ShoppingApp.Services.Product.API.Controllers
{
	[ApiController]
	[Route("api/v1/Product/[controller]")]
	[Produces("application/json")]
	public class CreateProductController : ControllerBase
	{
		private readonly IProductRepository _repository;

		public CreateProductController(IProductRepository repository)
		{
			_repository = repository;
		}

		[HttpPost]
		[ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.Created)]
		public async Task<ActionResult<ProductModel>> CreateProduct([FromBody] ProductModel product)
		{
			await _repository.CreateProduct(product);

			return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
		}
	}
}
