using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Models;
using Shopping.WebApp.Services;

namespace Shopping.WebApp.Pages;

public class OrderModel : PageModel
{
	private readonly IOrderApiService _orderApiService;

	public OrderModel(IOrderApiService orderApiService)
	{
		_orderApiService = orderApiService;
	}

	public IEnumerable<Order> Orders { get; set; } = new List<Order>();

	public async Task<IActionResult> OnGetAsync()
	{
		this.Orders = await this._orderApiService.GetUserOrders(Basket.DEFAULT_USER_NAME);

		return Page();
	}
}
