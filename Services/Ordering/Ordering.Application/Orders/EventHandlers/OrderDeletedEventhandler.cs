using Ordering.Application.Exception;
using Ordering.Application.Orders.Commands;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers;

public class DeleteOrderCommandHandler(IApplicationDbContext DbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderid = OrderId.Of(command.Id);
        var order = await DbContext.Orders.FindAsync([orderid], cancellationToken);
        if (order == null) { throw new OrderNotFoundException(command.Id); }
        DbContext.Orders.Remove(order);
        await DbContext.SaveChangesAsync(cancellationToken);
        return new DeleteOrderResult(true);
    }
}