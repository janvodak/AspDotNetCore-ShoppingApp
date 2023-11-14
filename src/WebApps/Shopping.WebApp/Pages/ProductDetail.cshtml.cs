using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Models;
using Shopping.WebApp.Services;

namespace Shopping.WebApp.Pages;

public class ProductDetailModel : PageModel
{
	private const int DEFAULT_PRODUCT_QUANTITY = 1;
	private const string DEFAULT_PRODUCT_COLOR = "Black";

	private readonly IProductApiService _productApiService;
	private readonly IBasketApiService _basketApiService;

	public ProductDetailModel(
		IProductApiService productApiService,
		IBasketApiService basketApiService)
	{
		this._productApiService = productApiService;
		this._basketApiService = basketApiService;
	}

	public Product Product { get; set; } = null!;

	[BindProperty]
	public string Color { get; set; } = null!;

	[BindProperty]
	public int Quantity { get; set; }

	public async Task<IActionResult> OnGetAsync(string? productId)
	{
		if (productId == null)
		{
			return NotFound();
		}

		this.Product = await this._productApiService.GetProductById(productId);

		if (this.Product == null)
		{
			return NotFound();
		}

		return Page();
	}

	public async Task<IActionResult> OnPostAddToBasketAsync(string productId)
	{
		Product product = await this._productApiService.GetProductById(productId);
		Basket basket = await this._basketApiService.GetBasket(Basket.DEFAULT_USER_NAME);

		BasketProduct basketProduct = new(
			productId,
			product.Name,
			ProductDetailModel.DEFAULT_PRODUCT_QUANTITY,
			product.Price,
			ProductDetailModel.DEFAULT_PRODUCT_COLOR);

		basket.Products.Add(basketProduct);

		await this._basketApiService.UpdateBasket(basket);

		return RedirectToPage("Basket");
	}
}
