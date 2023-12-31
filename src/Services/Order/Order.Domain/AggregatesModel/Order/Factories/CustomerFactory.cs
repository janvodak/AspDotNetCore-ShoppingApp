﻿using ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.ValueObjects;

namespace ShoppingApp.Services.Order.API.Domain.AggregatesModel.Order.Factories
{
	public class CustomerFactory : ICustomerFactory
	{
		public CustomerValueObject Create(string userName)
		{
			return new CustomerValueObject(userName);
		}
	}
}
