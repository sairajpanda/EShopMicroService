using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
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
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Input;
using Ordering.Application.Exception;

namespace Ordering.Application.Orders.Commands;

public record DeleteOrderCommand(Guid Id) : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsSuccess);

public class  DeleteCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Order Id cannot be empty");
    }
}
