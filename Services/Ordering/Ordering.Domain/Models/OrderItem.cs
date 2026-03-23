using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models;

public class OrderItem : Entity<Guid>
{
    public Guid ProductId { get; private set; } = default!;
    public Guid OrderId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal UnitPrice { get; private set; } = default!;
    internal OrderItem(Guid productId, Guid orderId, int quantity, decimal price)
    {
        ProductId = productId;
        OrderId = orderId;
        Quantity = quantity;
        UnitPrice = price;
    }
}
