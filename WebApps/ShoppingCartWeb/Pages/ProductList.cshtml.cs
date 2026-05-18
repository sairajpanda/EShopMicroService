using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCartWeb.Models.Basket;
using ShoppingCartWeb.Models.Catalog;
using ShoppingCartWeb.Services;

namespace ShoppingCartWeb.Pages;

public class ProductListModel(ILogger<IndexModel> logger, IBasketService _basketService, ICatalogService _catalogService) : PageModel
{
    public IEnumerable<Product> ProductList { get; set; } = [];
    public IEnumerable<string> CategoryList { get; set; } = [];


    [BindProperty(SupportsGet =true)]
    public string SelectedCategory { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(string CategoryName)
    {
        var response = await _catalogService.GetProducts();
        CategoryList = response.Products.SelectMany(P => P.Category!).Distinct();

        ProductList = response.Products.Where(p => p.Category!.Contains(CategoryName));
        SelectedCategory = CategoryName;

        return Page();
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
