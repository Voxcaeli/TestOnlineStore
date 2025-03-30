using FluentValidation;
using System.Net;
using System.Text.Json;
using TestOnlineStore.Persistence.Common.Exceptions;

namespace TestOnlineStore.WebAPI.Middlewares;

public class CustomExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandlerExceptionAsync(context, ex);
        }
    }

    private static Task HandlerExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        if (ex is ValidationException validationException)
        {
            code = HttpStatusCode.BadRequest;
            result = JsonSerializer.Serialize(validationException.Errors);
        }

        if (ex is NotFoundException notFoundException)
        {
            code = HttpStatusCode.NotFound;
            result = JsonSerializer.Serialize(notFoundException.Message);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(ex.Message);
        }

        return context.Response.WriteAsync(result);
    }
}
