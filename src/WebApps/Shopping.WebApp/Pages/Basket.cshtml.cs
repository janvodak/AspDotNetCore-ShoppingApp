using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Models;
using Shopping.WebApp.Services;

namespace Shopping.WebApp.Pages;

public class BasketModel : PageModel
{
	private readonly IBasketApiService _basketApiService;

	public BasketModel(IBasketApiService basketApiService)
	{
		this._basketApiService = basketApiService;
	}

	public Basket Basket { get; set; } = null!;

	public async Task<IActionResult> OnGetAsync()
	{
		this.Basket = await this._basketApiService.GetBasket(Basket.DEFAULT_USER_NAME);

		return Page();
	}

	public async Task<IActionResult> OnPostRemoveFromBasketAsync(string productId)
	{
		Basket basket = await this._basketApiService.GetBasket(Basket.DEFAULT_USER_NAME);

		BasketProduct basketProduct = basket.Products.Single(x => x.Id == productId);
		basket.Products.Remove(basketProduct);

		await this._basketApiService.UpdateBasket(basket);

		return RedirectToPage();
	}
}
