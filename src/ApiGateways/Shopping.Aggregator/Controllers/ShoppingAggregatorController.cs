using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects;
using ShoppingApp.ApiGateway.ShoppingAggregator.Models.Factories;

namespace ShoppingApp.ApiGateway.ShoppingAggregator.Controllers
{
	[ApiController]
	[Route("api/v1/ShoppingAggregator")]
	[Produces("application/json")]
	public class ShoppingAggregatorController : ControllerBase
	{
		private readonly IShoppingAggregateRootFactory _shoppingAggregateRootFactory;

		public ShoppingAggregatorController(IShoppingAggregateRootFactory shoppingAggregateRootFactory)
		{
			_shoppingAggregateRootFactory = shoppingAggregateRootFactory;
		}

		[HttpGet("{userName}")]
		[ProducesResponseType(typeof(ResponseDataTransferObject), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(ResponseDataTransferObject), (int)HttpStatusCode.InternalServerError)]
		public async Task<ActionResult<ResponseDataTransferObject>> GetUserdData(string userName)
		{
			ShoppingAggregateRoot shoppingAggregateRoot = await _shoppingAggregateRootFactory.Create(userName);

			ResponseDataTransferObject responseDataTransferObject = new()
			{
				Result = shoppingAggregateRoot
			};

			return Ok(responseDataTransferObject);
		}
	}
}
