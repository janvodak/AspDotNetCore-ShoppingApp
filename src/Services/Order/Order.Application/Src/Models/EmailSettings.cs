using System;
namespace Order.Application.Src.Models
{
	public class EmailSettings
	{
		public string ApiKey { get; set; } = null!;

		public string FromAddress { get; set; } = null!;

		public string FromName { get; set; } = null!;
	}
}
