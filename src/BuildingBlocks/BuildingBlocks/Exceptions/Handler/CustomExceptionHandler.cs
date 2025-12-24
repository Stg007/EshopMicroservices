
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

        (string Detail, string Title, int StatusCode) details = exception switch
        {
            InternalServerException =>
            (
            
                Detail: exception.Message,
                Title: exception.GetType().Name,
                StatusCode: StatusCodes.Status500InternalServerError
            ),
            FluentValidation.ValidationException =>
            (
                Detail: exception.Message,
                Title: exception.GetType().Name,
                StatusCode: StatusCodes.Status400BadRequest
            ),
            BadRequestException =>
            (
                Detail: exception.Message,
                Title: exception.GetType().Name,
                StatusCode: StatusCodes.Status400BadRequest
            ),
            NotFoundException =>
            (
                Detail: exception.Message,
                Title: exception.GetType().Name,
                StatusCode: StatusCodes.Status404NotFound
            ),
            _ =>
            (
                Detail: exception.Message,
                Title: exception.GetType().Name,
                StatusCode: StatusCodes.Status500InternalServerError
            )
        };


        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

        if (exception is FluentValidation.ValidationException validationException)
        {
            problemDetails.Extensions["ValidationErrors"] = validationException.Errors;
        }
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
