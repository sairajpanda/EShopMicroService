using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BuildingBlocks.Exception;

public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, System.Exception exception, CancellationToken cancellationToken)
    {
        Log.Error(exception, exception.Message);

        (string Detail, string Title, int StatusCode) details =
            exception switch
            {
                //InternalServerException =>
                //(
                //exception.Message,
                //exception.GetType().Name,
                //context.Response.StatusCode = StatusCodes.Status500InternalServerError
                //),
                //NotFoundException =>
                //(
                //exception.Message,
                //exception.GetType().Name,
                //context.Response.StatusCode = StatusCodes.Status404NotFound
                //),
                 _ =>
                (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };
        context.Response.StatusCode = details.StatusCode;

        var problemDetails = new ProblemDetails
        {
            Title = "An unexpected error occurred",
            Detail = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Instance = context.Request.Path
        };

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
