using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Product.API.Models;
using ShoppingApp.Services.Product.API.Repositories;

namespace ShoppingApp.Services.Product.API.Controllers
{
	[ApiController]
	[Route("api/v1/Product/[controller]")]
	[Produces("application/json")]
	public class GetProductsController : ControllerBase
	{
		private readonly IProductRepository _repository;

		public GetProductsController(IProductRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<ProductModel>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
		{
			var products = await _repository.GetProducts();
			return Ok(products);
		}
	}
}
