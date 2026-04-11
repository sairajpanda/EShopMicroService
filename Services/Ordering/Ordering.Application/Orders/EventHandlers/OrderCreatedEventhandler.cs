using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.Events;
using Microsoft.Extensions.Logging;
namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventhandler(ILogger<OrderCreatedEventhandler> logger) : INotificationHandler<OrderCreatedEvent>
{
   public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
   {
   logger.LogInformation("Order created event handled: {OrderId}", notification.order.Id);
   throw new NotImplementedException();
   }
}

