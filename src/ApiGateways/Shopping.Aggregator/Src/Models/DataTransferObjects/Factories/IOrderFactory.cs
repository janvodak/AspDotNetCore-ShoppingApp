namespace Shopping.Aggregator.Src.Models.DataTransferObjects.Factories
{
	public interface IOrderFactory
	{
		Task<IEnumerable<OrderDataTransferObject>> Create(string userName);
	}
}