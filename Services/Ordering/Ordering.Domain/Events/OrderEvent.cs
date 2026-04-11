
namespace Ordering.Domain.Events;

public record OrderCreatedEvent(Order order) : IDomainEvent;
public record OrderUpdatedEvent(Order order) : IDomainEvent;
public record OrderItemAddedEvent(OrderItem order) : IDomainEvent;
public record OrderItemRemovedEvent(OrderItem order) : IDomainEvent;

