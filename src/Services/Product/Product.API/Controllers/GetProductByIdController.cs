using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Product.API.Models.DataTransferObjects;
using ShoppingApp.Services.Product.API.Repositories;

namespace ShoppingApp.Services.Product.API.Controllers
{
	[ApiController]
	[Route("api/v1/Product")]
	[Produces("application/json")]
	public class GetProductByIdController : ControllerBase
	{
		private readonly IProductRepository _repository;
		private readonly ILogger<GetProductByIdController> _logger;

		public GetProductByIdController(
			IProductRepository repository,
			ILogger<GetProductByIdController> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[Route("[action]/{id:length(24)}", Name = "GetProductById")]
		[HttpGet]
		[ProducesResponseType(typeof(ResponseDataTransferObject), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<ActionResult<ProductDataTransferObject>> GetProductById(string id)
		{
			ProductDataTransferObject? productDataTransferObject = await _repository.GetProductByIdAsync(id);

			if (productDataTransferObject == null)
			{
				_logger.LogError("Product with id '{Id}' was not found.", id);

				ResponseDataTransferObject errorResponse = new(
					false,
					$"Product with id '{id}' was not found.");

				return BadRequest(errorResponse);
			}

			ResponseDataTransferObject response = new()
			{
				Result = productDataTransferObject
			};

			return Ok(response);
		}
	}
}
