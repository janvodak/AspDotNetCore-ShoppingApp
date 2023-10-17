namespace Order.Application.Src.Models
{
	public class Email
	{
		public string To { get; set; }

		public string Subject { get; set; }

		public string Body { get; set; }

		public Email(string to, string subject, string body)
		{
			this.To = to;
			this.Subject = subject;
			this.Body = body;
		}
	}
}
