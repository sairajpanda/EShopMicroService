using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace BuildingBlocks.Behaviour;

public class ValidationBehavior<Trequest, TResponse>
    (IEnumerable<IValidator<Trequest>> validators)
    : IPipelineBehavior<Trequest, TResponse>
    where Trequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle
        (Trequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<Trequest>(request);

        var validationResults =
            await Task.WhenAll(validators.Select(v => v.ValidateAsync(context,cancellationToken)));

        var failures = 
            validationResults.Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Any())
            throw new ValidationException(failures);

        return await next();
    }
}
