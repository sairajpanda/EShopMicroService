using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCartWeb.Models.Basket;
using ShoppingCartWeb.Services;

namespace ShoppingCartWeb.Pages.Shared
{
    public class CartModel(IBasketService _basketService) : PageModel
    {
        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();
        public async Task<IActionResult> OnGet()
        {
            Cart = await _basketService.GetUserBaskets();
            return Page();
        }


    }
}
