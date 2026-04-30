using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Messaging.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        //var command = MapToCreateOrderCommand(context.Message);
        //await sender.Send(command);
        logger.LogInformation("BasketCheckoutEvent handled successfully.");
    }
}
