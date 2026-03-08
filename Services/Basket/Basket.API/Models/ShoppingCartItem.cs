namespace Basket.API.Models;

public class ShoppingCartItem 
{
    public int Quantity { get; set; }

    public string Color { get; set; }
    public decimal Price {  get; set; }
    public Guid ProductId { get; set; }
    public String ProductName { get; set; }
}
