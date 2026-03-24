namespace Ordering.Domain.Models;
using Ordering.Domain.Events;


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
   public static Order Create (OrderId id, CustomerId customerId, OrderName orderName, Address billingAddress, Address shippingAddress, Payment payment)
    {
        var order=  new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            BillingAddress = billingAddress,
            ShippingAddress = shippingAddress,
            payment = payment,
            Status = OrderStatus.Draft
        };

        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }

    public static Order Update (Order order, OrderName orderName, Address billingAddress, Address shippingAddress, Payment payment)
    {
        order.OrderName = orderName;
        order.BillingAddress = billingAddress;
        order.ShippingAddress = shippingAddress;
        order.payment = payment;
        order.AddDomainEvent(new OrderUpdatedEvent(order));
        return order;
    }

    public void Add(ProductId productId, int quantity, decimal price)
    {
        var orderItem = new OrderItem(productId, this.Id, quantity, price);
        _ordersitems.Add(orderItem);
        this.AddDomainEvent(new OrderItemAddedEvent(orderItem));
    }
     public void Remove(OrderItemId orderItemId)
    {
        var orderItem = _ordersitems.FirstOrDefault(x => x.Id == orderItemId);
        if (orderItem != null)
        {
            _ordersitems.Remove(orderItem);
            this.AddDomainEvent(new OrderItemRemovedEvent(orderItem));
        }
    }

}
