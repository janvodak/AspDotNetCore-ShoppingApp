using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Models;
using Shopping.WebApp.Services;

namespace Shopping.WebApp.Pages;

public class CheckoutModel : PageModel
{
	private readonly IBasketApiService _basketApiService;

	public CheckoutModel(IBasketApiService basketApiService)
	{
		this._basketApiService = basketApiService;
	}

	[BindProperty]
	public CheckoutBasket CheckoutBasket { get; set; } = null!;

	public Basket Basket { get; set; } = null!;

	public async Task<IActionResult> OnGetAsync()
	{
		this.Basket = await this._basketApiService.GetBasket(Basket.DEFAULT_USER_NAME);

		return Page();
	}

	public async Task<IActionResult> OnPostCheckoutAsync()
	{
		this.Basket = await this._basketApiService.GetBasket(Basket.DEFAULT_USER_NAME);

		if (!ModelState.IsValid)
		{
			return Page();
		}

		this.CheckoutBasket.UserName = Basket.DEFAULT_USER_NAME;
		this.CheckoutBasket.TotalPrice = this.Basket.TotalPrice;

		await this._basketApiService.CheckoutBasket(this.CheckoutBasket);

		return RedirectToPage("Confirmation", "OrderSubmitted");
	}
}
