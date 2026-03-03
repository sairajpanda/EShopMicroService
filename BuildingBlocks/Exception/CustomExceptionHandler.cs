using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Serilog;
using FluentValidation;

namespace BuildingBlocks.Exception
{
    public class CustomExceptionHandler
        (ILogger<CustomExceptionHandler> logger)
    {
        /*public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception.Message,DateTime.UtcNow,
                "An unhandled exception occurred.");

           (string Details,string title,int StatusCode) details
                = exception switch
                {
                    InternalServerException
                }
        }*/
    }
}
