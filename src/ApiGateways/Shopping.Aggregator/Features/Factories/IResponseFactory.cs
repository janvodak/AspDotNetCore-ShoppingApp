namespace ShoppingApp.ApiGateway.ShoppingAggregator.Features.Factories
{
	public interface IResponseFactory
	{
		Task<T> Create<T>(HttpResponseMessage httpResponseMessage);
	}
}
