using System.ComponentModel.DataAnnotations;

namespace Basket.API.Models;

public class ShoppingCart 
{
    [Key]
    public Guid ShoppingCartId { get; set; }
    public string UserName { get; set; } = default!;
    public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

    public decimal TotalItemPrice { get; set; }
    public ShoppingCart(string _UserName)
    {
        UserName = _UserName;
    }
    public ShoppingCart()
    {
    }
}
