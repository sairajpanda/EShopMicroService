using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries;

public record GetOrdersByNameQuery(string Name) : IQuery<GetProductsByNameResult>;
public record GetProductsByNameResult(IEnumerable<OrderDto> Orders);

