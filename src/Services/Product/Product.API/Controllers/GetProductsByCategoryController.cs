using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Product.API.Models.DataTransferObjects;
using ShoppingApp.Services.Product.API.Repositories;

namespace ShoppingApp.Services.Product.API.Controllers
{
	[ApiController]
	[Route("api/v1/Product")]
	[Produces("application/json")]
	public class GetProductsByCategoryController : ControllerBase
	{
		private readonly IProductRepository _repository;

		public GetProductsByCategoryController(IProductRepository repository)
		{
			_repository = repository;
		}

		[Route("[action]/{category}")]
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<ResponseDataTransferObject>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<ProductDataTransferObject>>> GetProductsByCategory(string category)
		{
			IEnumerable<ProductDataTransferObject> productDataTransferObjects = await _repository.GetProductsByCategoryAsync(category);

			ResponseDataTransferObject response = new()
			{
				Result = productDataTransferObjects
			};

			return Ok(response);
		}
	}
}
