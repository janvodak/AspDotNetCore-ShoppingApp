namespace ShoppingApp.ApiGateway.ShoppingAggregator.Models.DataTransferObjects.Factories
{
	public interface IBasketFactory
	{
		Task<BasketDataTransferObject> Create(string userName);
	}
}