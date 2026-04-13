using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries;

public record GetOrdersByNameQuery(string Name) : IQuery<GetProductsByNameResult>;
public record GetProductsByNameResult(IEnumerable<OrderDto> Orders);
public class GetOrdersByNameHandlers(IApplicationDbContext DbContext) : IQueryHandler<GetOrdersByNameQuery, GetProductsByNameResult>
{
    public async Task<GetProductsByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await DbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(p => p.OrderName.Value.Contains(query.Name))
            .OrderBy(p => p.OrderName.Value)
            .ToListAsync(cancellationToken);

        var OrderDto = OrderExtensions.ProjectToOrderDto(orders);
        return new GetProductsByNameResult(OrderDto);
    }
}
