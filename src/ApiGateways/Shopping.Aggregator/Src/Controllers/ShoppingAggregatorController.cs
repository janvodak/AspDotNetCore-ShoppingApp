using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Src.Factories;
using Shopping.Aggregator.Src.Models;

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
			this._shoppingAggregateRootFactory = shoppingAggregateRootFactory;
		}

		[HttpGet("{userName}")]
		[ProducesResponseType(typeof(ShoppingAggregateRoot), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(ShoppingAggregateRoot), (int)HttpStatusCode.InternalServerError)]
		public async Task<ActionResult<ShoppingAggregateRoot>> GetUserdData(string userName)
		{
			ShoppingAggregateRoot shoppingAggregateRoot = await this._shoppingAggregateRootFactory.Create(userName);

			return Ok(shoppingAggregateRoot);
		}
	}
}
