using BuildingBlocks.CQRS;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);


public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.order).NotNull().WithMessage("Order cannot be null");
        RuleFor(x => x.order.OrderItems).NotEmpty().WithMessage("Order must have at least one item");
        RuleFor(x => x.order.CustomerId).NotEmpty().WithMessage("CustomerId cannot be empty");
        RuleFor(x => x.order.OrderName).NotEmpty().WithMessage("OrderName cannot be empty");
        RuleFor(x => x.order.BillingAddress).NotNull().WithMessage("BillingAddress cannot be null");
        RuleFor(x => x.order.ShippingAddress).NotNull().WithMessage("ShippingAddress cannot be null");
        RuleFor(x => x.order.Payment).NotNull().WithMessage("Payment cannot be null");
    }
}

public class UpdateOrderCommandHandler(IApplicationDbContext DbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderid = OrderId.Of(command.order.Id);
        var order = await DbContext.Orders.FindAsync([orderid], cancellationToken);
        if (order != null) { throw new OrderNotFoundException(command.order.Id); }

        UpdateOrderWithNewValues(order, command.order);
        DbContext.Orders.Update(order);
        await DbContext.SaveChangesAsync(cancellationToken);
        return new UpdateOrderResult(true);
    }

    public Order UpdateOrderWithNewValues(Order order, OrderDto _order)
    {
        //order.OrderName = OrderName.Of(_order.OrderName);
        order.Update(order, OrderName.Of(_order.OrderName), Address.FromDto(_order.BillingAddress), Address.FromDto(_order.ShippingAddress), Payment.FromDto(_order.Payment));
        return order;
    }
}