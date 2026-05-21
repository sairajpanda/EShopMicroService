using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Orders.Commands;
using Ordering.Domain.Enum;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        try
        {
            //to create new order and start order fullfillment process
            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);
            logger.LogInformation("BasketCheckoutEvent handled successfully.");
        }
        catch (System.Exception ex)
        {

            throw;
        }

    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {

        AddressDto BillingAddressDto = new AddressDto(message.FirstName,
         message.LastName,
         message.Street,
         message.City,
         message.State,
         message.Country,
         message.ZipCode,
         message.EmailAddress!);

        AddressDto ShippingAddress = new AddressDto(message.FirstName,
          message.LastName,
          message.Street,
          message.City,
          message.State,
          message.Country,
          message.ZipCode,
          message.EmailAddress!);

        PaymentDto PaymentDto = new PaymentDto(message.CardName, message.CardNumber, message.CardHolderName, message.ExpirationDate, message.CVV, message.PaymentMethod);

        var orderID = Guid.NewGuid();
        List<OrderItemDto> OrderItems = new List<OrderItemDto>();

        OrderDto orderDto = new OrderDto(
            Id: orderID,
            CustomerId: message.CustermId,
            OrderName: message.UserName,
            BillingAddress: BillingAddressDto,
            ShippingAddress: ShippingAddress,
            Payment: PaymentDto,
            Status: Ordering.Domain.Enum.OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto
                (
                   orderID,
                   new Guid("60B1F787-E9B3-459A-8F1E-A28A125C0212"),
                   1,
                   200
                ),
                new OrderItemDto
                (
                    orderID,
                    new Guid("60B1F787-E9B3-459A-8F1E-A28A125C02E9"),
                    1,
                    2000
                )
            ]);
            return new CreateOrderCommand(orderDto);
    }
}
