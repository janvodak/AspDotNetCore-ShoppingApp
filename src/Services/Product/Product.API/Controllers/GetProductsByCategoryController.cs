using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Product.API.Models;
using ShoppingApp.Services.Product.API.Repositories;

namespace ShoppingApp.Services.Product.API.Controllers
{
	[ApiController]
	[Route("api/v1/Product/[controller]")]
	[Produces("application/json")]
	public class GetProductsByCategoryController : ControllerBase
	{
		private readonly IProductRepository _repository;

		public GetProductsByCategoryController(IProductRepository repository)
		{
			_repository = repository;
		}

		[HttpGet("{category}")]
		[ProducesResponseType(typeof(IEnumerable<ProductModel>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductsByCategory(string category)
		{
			IEnumerable<ProductModel> product = await _repository.GetProductsByCategory(category);

			return Ok(product);
		}
	}
}
