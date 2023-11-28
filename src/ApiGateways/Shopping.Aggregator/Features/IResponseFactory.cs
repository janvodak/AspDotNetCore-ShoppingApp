namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features
{
	public interface IResponseFactory
	{
		Task<T> Create<T>(HttpResponseMessage httpResponseMessage);
	}
}
