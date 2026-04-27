using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers;

public class GetOrdersHandlers(IApplicationDbContext DbContext) :
    IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.Pagination.pageIndex;

        var pageSize = query.Pagination.pageSize;

        var totalCount = await DbContext.Orders.CountAsync(cancellationToken);

        var orders = await DbContext.Orders
        //.ToListAsync(cancellationToken);
        .Include(o => o.OrderItems)
        .OrderBy(o => o.OrderName.Value)
        .Skip(pageSize * pageIndex)
        .Take(pageSize)
        .ToListAsync(cancellationToken);

        return new GetOrdersResult(new PaginatedResult<OrderDto>(pageIndex, pageSize, totalCount, OrderExtensions.ProjectToOrderDto(orders)));

    }
}