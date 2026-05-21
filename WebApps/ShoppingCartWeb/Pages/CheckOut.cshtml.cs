using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCartWeb.Models.Basket;
using ShoppingCartWeb.Services;

namespace ShoppingCartWeb.Pages;

public class CheckOutModel(ILogger<IndexModel> logger, IBasketService basketService, ICatalogService catalogService) : PageModel
{

    [BindProperty]
    public BasketCheckoutModel Order { get; set; } = new BasketCheckoutModel();

    [BindProperty]
    public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await basketService.GetUserBaskets();
        return Page();
    }

    public async Task<IActionResult> OnPostCheckOutAsync()
    {
        try
        {
            Cart = await basketService.GetUserBaskets();

            if (Cart == null || !Cart.Items.Any())
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty.");
                return Page();
            }

            Order.CustermId = new Guid("60B1F787-E9B3-459A-8F1E-A28A125C02E7");
            Order.UserName = "SairajPanda";
            Order.TotalPrice = Cart.TotalItemPrice;
            Order.Street = "Street";
            Order.City = "City";
            Order.State = "California";
            Order.CardHolderName = "SairajPanda";
            Order.PaymentMethod = 1;

            await basketService.BasketCheckOut(new CheckOutBasketRequest(Order));

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
        catch (Exception ex)
        {

            throw;
        }
    }

}
