using Shopping.Aggregator.Src.Models.DataTransferObjects;

namespace Shopping.Aggregator.Src.Features
{
	public interface IHttpRequestMessageFactory
	{
		HttpRequestMessage Create(RequestDataTransferObject request);
	}
}
