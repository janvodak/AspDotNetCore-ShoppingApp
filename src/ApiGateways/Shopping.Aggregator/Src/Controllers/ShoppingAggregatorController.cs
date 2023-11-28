using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Src.Factories;
using Shopping.Aggregator.Src.Models;
using Shopping.Aggregator.Src.Models.DataTransferObjects;

namespace Shopping.Aggregator.Src.Controllers
{
	[ApiController]
	[Route("api/v1/shopping/[action]")]
	[Produces("application/json")]
	public class ShoppingAggregatorController : ControllerBase
	{
		private readonly ShoppingAggregateRootFactory _shoppingAggregateRootFactory;

		public ShoppingAggregatorController(ShoppingAggregateRootFactory shoppingAggregateRootFactory)
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
