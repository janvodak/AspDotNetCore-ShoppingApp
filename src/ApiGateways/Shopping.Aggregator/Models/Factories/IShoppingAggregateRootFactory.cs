namespace ShoppingApp.ApiGateway.ShoppingAggregator.Models.Factories
{
	public interface IShoppingAggregateRootFactory
	{
		Task<ShoppingAggregateRoot> Create(string userName);
	}
}