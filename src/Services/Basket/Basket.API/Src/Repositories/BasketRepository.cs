using Basket.API.Src.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Src.Repositories
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDistributedCache _distributedCache;

		public BasketRepository(IDistributedCache distributedCache)
		{
			this._distributedCache = distributedCache;
		}

		public async Task DeleteBasket(string userName)
		{
			await this._distributedCache.RemoveAsync(userName);
		}

		public async Task<BasketEntity?> GetBasket(string userName)
		{
			string? basket = await this._distributedCache.GetStringAsync(userName);

			if (String.IsNullOrEmpty(basket))
			{
				return null;
			}

			return JsonConvert.DeserializeObject<BasketEntity>(basket);
		}

		public async Task<BasketEntity?> UpdateBasket(BasketEntity basket)
		{
			await this._distributedCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

			return await this.GetBasket(basket.UserName);
		}
	}
}
