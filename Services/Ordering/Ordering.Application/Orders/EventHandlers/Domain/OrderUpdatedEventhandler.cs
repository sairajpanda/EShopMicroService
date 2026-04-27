using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.Events;
using Microsoft.Extensions.Logging;
namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderUpdatedEventhandler(ILogger<OrderUpdatedEventhandler> logger) : INotificationHandler<OrderUpdatedEvent>
{
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Order updated event handled: {OrderId}", notification.order.Id);
            return Task.CompletedTask;
        }
}
