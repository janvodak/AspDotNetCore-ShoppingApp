namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features
{
	public interface IHttpResponseParser
	{
		Task<T> ParseResponse<T>(HttpResponseMessage response);
	}
}
