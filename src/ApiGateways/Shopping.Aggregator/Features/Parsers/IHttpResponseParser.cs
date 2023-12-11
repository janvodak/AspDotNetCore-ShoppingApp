namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features.Parsers
{
	public interface IHttpResponseParser
	{
		Task<T> ParseResponse<T>(HttpResponseMessage response);
	}
}
