using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCartWeb.Models.Catalog;
using ShoppingCartWeb.Services;
namespace ShoppingCartWeb.Pages;

public class ProductDetailModel(ILogger<IndexModel> logger, IBasketService _basketService, ICatalogService _catalogService) : PageModel
{
    public Product Product { get; set; } = default!;

    [BindProperty]
    public string Color { get; set; } = default!;

    [BindProperty]
    public string Quantity { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid productID)
    {
        var response = await _catalogService.GetProduct(productID);
        Product = response._products;
        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        var UserBasketDetails = await _basketService.GetUserBaskets();
        var ProductDetails = await _catalogService.GetProduct(productId);
        UserBasketDetails.Items.Add(new ShoppingCartWeb.Models.Basket.ShoppingCartItemModel
        {
            ProductName = ProductDetails._products.Name,
            Quantity = int.Parse(Quantity),
            Price = (decimal)ProductDetails._products.Price,
            Color = Color,
            ProductId = productId
        });
        var UserStoreBasket = await _basketService.StoreBasket(new ShoppingCartWeb.Models.Basket.StoreBasketRequest(
           "SairajPanda",
           UserBasketDetails.Items,
           UserBasketDetails.Items.Sum(x => x.Price * x.Quantity)
        ));
        return RedirectToPage("Cart");
    }

}
