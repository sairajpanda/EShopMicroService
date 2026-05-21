using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCartWeb.Models.Basket;
using ShoppingCartWeb.Models.Catalog;
using ShoppingCartWeb.Models.Order;
using ShoppingCartWeb.Services;

namespace ShoppingCartWeb.Pages;

public class ProductListModel(ILogger<IndexModel> logger, IBasketService _basketService, ICatalogService _catalogService) : PageModel
{
    public IEnumerable<Product> ProductList { get; set; } = new List<Product>();
    public IEnumerable<string> CategoryList { get; set; } = new List<string>();


    [BindProperty(SupportsGet =true)]
    public string SelectedCategory { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync()
    {
        var response = await _catalogService.GetProducts();

        CategoryList = response.Products
            .SelectMany(p => p.Category ?? new List<string>())
            .Distinct();

        if (string.IsNullOrWhiteSpace(SelectedCategory))
        {
            ProductList = response.Products;
        }
        else
        {
            ProductList = response.Products
                .Where(p =>
                    p.Category != null &&
                    p.Category.Contains(SelectedCategory));
        }

        return Page();
    }


    public async Task<IActionResult> OnPostProductAsync(string category)
    {
        Console.WriteLine(category.ToString());
        return RedirectToPage("Cart");
    }


    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        var UserBasketDetails = await _basketService.GetUserBaskets();
        var ProductDetails = await _catalogService.GetProduct(productId);

        UserBasketDetails.Items.Add(new ShoppingCartItemModel
        {
            ProductName = ProductDetails._products.Name,
            Quantity = 1,
            Price = (decimal)ProductDetails._products.Price,
            Color = "Black",
            ProductId = productId
        });
        var UserStoreBasket = await _basketService.StoreBasket(new StoreBasketRequest(
           "SairajPanda",
           UserBasketDetails.Items,
           UserBasketDetails.Items.Sum(x => x.Price * x.Quantity)
        ));
        return RedirectToPage("Cart");
    }
}
