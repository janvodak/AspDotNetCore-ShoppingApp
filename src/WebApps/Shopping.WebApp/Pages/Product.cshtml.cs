using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Models;
using Shopping.WebApp.Services;

namespace Shopping.WebApp.Pages;

public class ProductModel : PageModel
{
	private const int DEFAULT_PRODUCT_QUANTITY = 1;
	private const string DEFAULT_PRODUCT_COLOR = "Black";

	private readonly IProductApiService _productApiService;
	private readonly IBasketApiService _basketApiService;

	public ProductModel(
		IProductApiService productApiService,
		IBasketApiService basketApiService)
	{
		this._productApiService = productApiService;
		this._basketApiService = basketApiService;
	}

	public IEnumerable<string> CategoryList { get; set; } = new List<string>();
	public IEnumerable<Product> ProductList { get; set; } = new List<Product>();

	[BindProperty(SupportsGet = true)]
	public string? SelectedCategory { get; set; }

	public async Task<IActionResult> OnGetAsync(string? categoryName)
	{
		IEnumerable<Product> productList = await this._productApiService.GetProducts();
		this.CategoryList = productList.Select(p => p.Category).Distinct();

		if (string.IsNullOrWhiteSpace(categoryName) == false)
		{
			this.ProductList = productList.Where(p => p.Category == categoryName);
			this.SelectedCategory = categoryName;
		} else
		{
			this.ProductList = productList;
			this.SelectedCategory = null;
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
			ProductModel.DEFAULT_PRODUCT_QUANTITY,
			product.Price,
			ProductModel.DEFAULT_PRODUCT_COLOR);

		basket.Products.Add(basketProduct);

		await this._basketApiService.UpdateBasket(basket);

		return RedirectToPage("Basket");
	}
}
