using System.ComponentModel.DataAnnotations;

namespace Basket.API.Models;

public class ShoppingCartItem 
{
    [Key]
    public Guid ShoppingCartItemId { get; set; }
    public int Quantity { get; set; }
    public string Color { get; set; }
    public decimal Price {  get; set; }
    public Guid ProductId { get; set; }
    public String ProductName { get; set; }
    public Guid ShoppingCartId { get; set; }
}
