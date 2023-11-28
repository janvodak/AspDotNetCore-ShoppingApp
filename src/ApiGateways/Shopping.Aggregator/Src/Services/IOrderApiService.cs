using Shopping.Aggregator.Src.Models.DataTransferObjects;

namespace Shopping.Aggregator.Src.Services
{
	public interface IOrderApiService
	{
		Task<IEnumerable<OrderDataTransferObject>> GetUserOrdersAsync(string username);
	}
}
