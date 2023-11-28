namespace Shopping.Aggregator.Src.Models.DataTransferObjects.Factories
{
	public interface IBasketFactory
	{
		Task<BasketDataTransferObject> Create(string userName);
	}
}