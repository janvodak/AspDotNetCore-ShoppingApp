using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Models;
using Shopping.WebApp.Services;

namespace Shopping.WebApp.Pages;

public class IndexModel : PageModel
{
	private const int DEFAULT_PRODUCT_QUANTITY = 1;
	private const string DEFAULT_PRODUCT_COLOR = "Black";

	private readonly IProductApiService _productApiService;
	private readonly IBasketApiService _basketApiService;

	public IndexModel(
		IProductApiService productApiService,
		IBasketApiService basketApiService)
	{
		this._productApiService = productApiService;
		this._basketApiService = basketApiService;
	}

	public IEnumerable<Product> ProductList { get; set; } = new List<Product>();

	public async Task<IActionResult> OnGetAsync()
	{
		this.ProductList = await this._productApiService.GetProducts();
		return Page();
	}

	public async Task<IActionResult> OnPostAddToBasketAsync(string productId)
	{
		Product? product = await this._productApiService.GetProductById(productId);
		Basket basket = await this._basketApiService.GetBasket(Basket.DEFAULT_USER_NAME);

		if (product == null)
		{
			return NotFound();
		}

		BasketProduct basketProduct = new(
			productId,
			product.Name,
			IndexModel.DEFAULT_PRODUCT_QUANTITY,
			product.Price,
			IndexModel.DEFAULT_PRODUCT_COLOR);

		basket.Products.Add(basketProduct);

		await this._basketApiService.UpdateBasket(basket);

		return RedirectToPage("Basket");
	}
}

