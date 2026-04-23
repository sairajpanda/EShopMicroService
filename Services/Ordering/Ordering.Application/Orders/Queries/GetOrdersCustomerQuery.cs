using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries;

public record GetOrdersCustomerQuery(Guid CustomerId) :
    IQuery<GetOrdersCustomerResult>;

public record GetOrdersCustomerResult(IEnumerable<OrderDto> Orders);

public class GetOrdersCustomerHandlers(IApplicationDbContext DbContext) :
    IQueryHandler<GetOrdersCustomerQuery, GetOrdersCustomerResult>
{
    public async Task<GetOrdersCustomerResult> Handle
        (GetOrdersCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await DbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(p => p.CustomerId ==CustomerId.Of(query.CustomerId))
            .OrderBy(p => p.OrderName.Value)
            .ToListAsync(cancellationToken);
        var OrderDto = OrderExtensions.ProjectToOrderDto(orders);
        return new GetOrdersCustomerResult(OrderDto);
    }
}