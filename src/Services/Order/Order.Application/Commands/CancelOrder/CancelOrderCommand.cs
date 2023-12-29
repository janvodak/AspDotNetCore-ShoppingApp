﻿using System.Runtime.Serialization;
using MediatR;

namespace ShoppingApp.Services.Order.API.Application.Commands.DeleteOrder
{
	public class CancelOrderCommand : IRequest<bool>
	{
		[DataMember]
		public int Id { get; private set; }

		public CancelOrderCommand(int id)
		{
			Id = id;
		}
	}
}