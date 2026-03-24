namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _ordersitems = new List<OrderItem>();
    public IReadOnlyList<OrderItem> OrderItems => _ordersitems;
    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Payment payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = default!;
    public decimal TotalPrice {
    get => OrderItems.Sum(x => x.UnitPrice * x.Quantity);private set { }
    }
    public void AddOrders(OrderItem orderItem)
    {
        _ordersitems.Add(orderItem);
    }
    public void RemoveOrders(OrderItem orderItem)
    {
        var Item = _ordersitems.FirstOrDefault(x=> x.Id == orderItem.Id);
        if (Item != null) { _ordersitems.Remove(Item);}
    }
}
