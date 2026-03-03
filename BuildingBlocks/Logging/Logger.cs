using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BuildingBlocks.Logging;

public class LoggerBehaviour<TRequest, TResponse>
    (ILogger<LoggerBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle
    (TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[Start] Handling {RequestName} with content: {@Request}", 
            typeof(TRequest).Name, request);

        var timer = new Stopwatch();
        timer.Start ();

        var response = await next();
        timer.Stop ();

        var timetaken = timer.Elapsed;
        if(timetaken.Seconds>3)
            logger.LogInformation("Handled {RequestName} in {TimeTaken} seconds", 
                typeof(TRequest).Name, timetaken.TotalSeconds);

        logger.LogInformation("[End]");
        return response;
    }
}
