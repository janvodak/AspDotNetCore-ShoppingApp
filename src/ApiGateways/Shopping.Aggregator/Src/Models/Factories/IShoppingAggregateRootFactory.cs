namespace Shopping.Aggregator.Src.Models.Factories
{
	public interface IShoppingAggregateRootFactory
	{
		Task<ShoppingAggregateRoot> Create(string userName);
	}
}