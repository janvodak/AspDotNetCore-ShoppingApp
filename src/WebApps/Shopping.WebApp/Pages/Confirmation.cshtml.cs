using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.WebApp.Pages
{
	public class ConfirmationModel : PageModel
	{
		public string Message { get; set; } = null!;

		public void OnGetContact()
		{
			this.Message = "Your email was sent.";
		}

		public void OnGetOrderSubmitted()
		{
			this.Message = "Your order submitted successfully.";
		}
	}
}
