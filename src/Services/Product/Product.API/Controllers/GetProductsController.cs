using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Product.API.Models.DataTransferObjects;
using ShoppingApp.Services.Product.API.Repositories;

namespace ShoppingApp.Services.Product.API.Controllers
{
	[ApiController]
	[Route("api/v1/Product")]
	[Produces("application/json")]
	public class GetProductsController : ControllerBase
	{
		private readonly IProductRepository _repository;

		public GetProductsController(IProductRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<ResponseDataTransferObject>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<ProductDataTransferObject>>> GetProducts()
		{
			IEnumerable<ProductDataTransferObject> productDataTransferObjects = await _repository.GetProductsAsync();

			ResponseDataTransferObject response = new()
			{
				Result = productDataTransferObjects
			};

			return Ok(response);
		}
	}
}
