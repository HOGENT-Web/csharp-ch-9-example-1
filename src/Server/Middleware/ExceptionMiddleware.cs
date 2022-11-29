using System;
using System.Net;
using BogusStore.Domain.Exceptions;
using BogusStore.Shared.Infrastructure;

namespace BogusStore.Server.Middleware;

/// <summary>
/// Global error handling middleware, when an exception is thrown in the underlying layers,
/// we intercept it and return a more appropriate error without a stack trace.
/// </summary>
public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> logger;
    private readonly RequestDelegate next;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
    {
        this.logger = logger;
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Something went wrong");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ErrorDetails error = exception switch
        {
            EntityNotFoundException ex => new ErrorDetails(ex.Message, HttpStatusCode.NotFound),
            EntityAlreadyExistsException ex => new ErrorDetails(ex.Message, HttpStatusCode.Conflict),
            // Add more custom exceptions here...
            ApplicationException ex => new ErrorDetails(ex.Message),
            _ => new ErrorDetails(exception.Message)
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)error.StatusCode;
        await context.Response.WriteAsync(error.ToString());
    }
}
