using Ordering.Application.Orders.Queries;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers;

public class GetOrdersCustomerHandlers(IApplicationDbContext DbContext) :
    IQueryHandler<GetOrdersCustomerQuery, GetOrdersCustomerResult>
{
    public async Task<GetOrdersCustomerResult> Handle
        (GetOrdersCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await DbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(p => p.CustomerId == CustomerId.Of(query.CustomerId))
            .OrderBy(p => p.OrderName.Value)
            .ToListAsync(cancellationToken);
        var OrderDto = OrderExtensions.ProjectToOrderDto(orders);
        return new GetOrdersCustomerResult(OrderDto);
    }
}
