using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.Events;
using Microsoft.Extensions.Logging;
using MassTransit;
using Microsoft.FeatureManagement;
namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventhandler
    (ILogger<OrderCreatedEventhandler> logger,
    IPublishEndpoint publishEndpoint,
    IFeatureManager featureManager) 
    : INotificationHandler<OrderCreatedEvent>
{
   public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
   {
        if (await featureManager.IsEnabledAsync("OrderFullfillment"))
        {
            logger.LogInformation("Order created event handled: {OrderId}", domainEvent.order.Id);
            //var orderCreatedIntegratedEvent = domainEvent.order.ToOrderDto();
            //await publishEndpoint.Publish(orderCreatedIntegratedEvent, cancellationToken);
        }
    }
}

