using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries;

public record GetOrdersCustomerQuery(Guid CustomerId) :
    IQuery<GetOrdersCustomerResult>;

public record GetOrdersCustomerResult(IEnumerable<OrderDto> Orders);

