namespace Shopping.Aggregator.Src.Features
{
	public interface IResponseFactory
	{
		Task<T> Create<T>(HttpResponseMessage httpResponseMessage);
	}
}
