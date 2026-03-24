using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    public ProductId ProductId { get; private set; } = default!;
    public OrderId OrderId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal UnitPrice { get; private set; } = default!;
    internal OrderItem(ProductId productId, OrderId orderId, int quantity, decimal price)
    {
        ProductId = productId;
        OrderId = orderId;
        Quantity = quantity;
        UnitPrice = price;
        Id= OrderItemId.Of(Guid.NewGuid());
    }
}
