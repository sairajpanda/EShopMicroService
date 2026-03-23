namespace Ordering.Domain.Models;

public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> _ordersitems = new List<OrderItem>();
    public IReadOnlyList<OrderItem> OrderItems => _ordersitems;
    public Guid CustomerId { get; private set; } = default!;
    public string OrderName { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Payment payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = default!;
    public decimal TotalPrice {
    get => OrderItems.Sum(x => x.UnitPrice * x.Quantity);private set { }
    }
}
