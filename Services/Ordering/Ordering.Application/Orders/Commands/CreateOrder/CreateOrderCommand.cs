using BuildingBlocks.CQRS;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult (Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order).NotNull().WithMessage("Order cannot be null");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order must have at least one item");
        RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId cannot be empty");
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("OrderName cannot be empty");
        RuleFor(x => x.Order.BillingAddress).NotNull().WithMessage("BillingAddress cannot be null");
        RuleFor(x => x.Order.ShippingAddress).NotNull().WithMessage("ShippingAddress cannot be null");
        RuleFor(x => x.Order.Payment).NotNull().WithMessage("Payment cannot be null");
    }
}

public class CreateOrderCommandHandler (IApplicationDbContext DbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);
        DbContext.Orders.Add(order);
        await DbContext.SaveChangesAsync(cancellationToken);
        return new CreateOrderResult(order.Id.Value);
    }


private Order CreateNewOrder(OrderDto orderDto)
    {
        var BillingAddress =
           Address.Of(orderDto.ShippingAddress.FirstName, 
           orderDto.ShippingAddress.LastName,
           orderDto.ShippingAddress.Street,
           orderDto.ShippingAddress.City,
           orderDto.ShippingAddress.State,
           orderDto.ShippingAddress.Country,
           orderDto.ShippingAddress.ZipCode,
           orderDto.ShippingAddress.EmailAddress);
        
        var ShippingAddress =
   Address.Of(orderDto.BillingAddress.FirstName,
   orderDto.BillingAddress.LastName,
   orderDto.BillingAddress.Street,
   orderDto.BillingAddress.City,
   orderDto.BillingAddress.State,
   orderDto.BillingAddress.Country,
   orderDto.BillingAddress.ZipCode,
   orderDto.BillingAddress.EmailAddress);

        var NewOrder = Order.Create(
            OrderId.Of(Guid.NewGuid()),
            CustomerId.Of(orderDto.CustomerId),
            OrderName.Of(orderDto.OrderName),
            BillingAddress,
            ShippingAddress,
            Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.CardHolderName, 
            orderDto.Payment.ExpirationDate, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod));

        foreach (var OrderitemDto in orderDto.OrderItems)
        {
            NewOrder.Add(ProductId.Of(OrderitemDto.ProductId), OrderitemDto.Quantity, OrderitemDto.UnitPrice);
        }
        return NewOrder;
    }
}