using System.ComponentModel.DataAnnotations;

namespace ShoppingCartWeb.Models.Basket;

public class ShoppingCartModel
{
    public string UserName { get; set; } = default!;
    public ICollection<ShoppingCartItemModel> Items { get; set; }
    public decimal TotalItemPrice { get; set; }
}
public class ShoppingCartItemModel
{
    [Key]
    public Guid ShoppingCartItemId { get; set; }
    public int Quantity { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public Guid ProductId { get; set; }
    public String ProductName { get; set; }
}

public record GetBasketResponse(ShoppingCartModel _shoppingCart);
public record StoreBasketRequest(string UserName,ICollection<ShoppingCartItemModel> Items,decimal TotalItemPrice);
public record StoreBasketResponse(string UserName);
public record DeleteBasketResponse(bool IsSuccess);




