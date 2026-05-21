using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCartWeb.Models.Basket;
using ShoppingCartWeb.Models.Order;
using ShoppingCartWeb.Services;

namespace ShoppingCartWeb.Pages;

public class OrderListModel(ILogger<IndexModel> logger, IBasketService basketService, ICatalogService catalogService, IOrderService orderService) : PageModel
{
    public IEnumerable<OrderDto> Orders { get; set; } = new List<OrderDto>();

    public async Task<IActionResult> OnGet()
    {
        var response = await orderService.GetOrdersByCustomer(new Guid("60B1F787-E9B3-459A-8F1E-A28A125C02E7"));
        Orders = response.Orders;
        return Page();
    }
}
