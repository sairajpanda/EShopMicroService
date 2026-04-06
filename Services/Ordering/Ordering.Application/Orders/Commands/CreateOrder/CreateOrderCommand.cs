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
}

private Order CreateNewOrder(OrderDto orderDto)
    {
        var BillingAddress =
           Address.Of(orderDto.BillingAddress.FirstName, 
           orderDto.BillingAddress.LastName, 
           orderDto.BillingAddress.State, 
           orderDto.BillingAddress.City, 
           orderDto.BillingAddress.Country);


        string firstName, string lastName, string street, string city, string state, string country, string zipCode, string? emailAddress
        //var order = new Domain.Models.Order
        //{
        //Id = Guid.NewGuid();
        //CustomerId = orderDto.CustomerId;
        //OrderName = orderDto.OrderName;

        //    ShippingAddress = new Domain.Models.Address
        //    {
        //        Street = orderDto.ShippingAddress.Street,
        //        City = orderDto.ShippingAddress.City,
        //        State = orderDto.ShippingAddress.State,
        //        ZipCode = orderDto.ShippingAddress.ZipCode,
        //        Country = orderDto.ShippingAddress.Country
        //    },
        //    Payment = new Domain.Models.Payment
        //    {
        //        PaymentMethod = orderDto.Payment.PaymentMethod,
        //        Amount = orderDto.Payment.Amount,
        //        Currency = orderDto.Payment.Currency
        //    },
        //    Status = Domain.Enum.OrderStatus.Pending
        //};
        //foreach (var item in orderDto.OrderItems)
        //{
        //    var orderItem = new Domain.Models.OrderItem
        //    {
        //        ProductId = item.ProductId,
        //        Quantity = item.Quantity,
        //        UnitPrice = item.UnitPrice
        //    };
        //    order.OrderItems.Add(orderItem);
        //}
        Order order = new Order();
        return order;
    }
}