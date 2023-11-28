using Shopping.Aggregator.Src.Models.DataTransferObjects;

namespace Shopping.Aggregator.Src.Services
{
	public interface IBaseService
	{
		Task<HttpResponseMessage> SendAsync(string clientName, RequestDataTransferObject request);
	}
}
