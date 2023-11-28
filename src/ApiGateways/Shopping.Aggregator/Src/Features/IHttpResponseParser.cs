namespace Shopping.Aggregator.Src.Features
{
	public interface IHttpResponseParser
	{
		Task<T> ParseResponse<T>(HttpResponseMessage response);
	}
}
