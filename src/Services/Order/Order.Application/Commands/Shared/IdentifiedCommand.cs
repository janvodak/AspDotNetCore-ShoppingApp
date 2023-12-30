using MediatR;

namespace ShoppingApp.Services.Order.API.Application.Commands.Shared
{
	public class IdentifiedCommand<T, R> : IRequest<R> where T : IRequest<R>
	{
		public T Command { get; private set; }

		public Guid Id { get; private set; }

		public IdentifiedCommand(T command, Guid id)
		{
			Command = command;
			Id = id;
		}
	}
}
