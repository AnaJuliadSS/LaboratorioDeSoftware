using GasturaApp.Infrastructure.Exceptions;
using System.Net;
using System.Text.Json;

namespace GasturaApp.Infrastructure.Middlewares;

public class ExceptionMiddleware : IMiddleware
{

    private readonly Dictionary<Type, HttpStatusCode> _exceptionStatusCodes = new()
    {
        { typeof(CampoObrigatorioException), HttpStatusCode.BadRequest },
        { typeof(CampoInvalidoException), HttpStatusCode.BadRequest },
        { typeof(EntidadeNaoEncontradaException), HttpStatusCode.NotFound },
    };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = _exceptionStatusCodes.TryGetValue(exception.GetType(), out var code)
            ? code
            : HttpStatusCode.InternalServerError;

        context.Response.ContentType = "application/json";  
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            status = context.Response.StatusCode,
            error = exception.Message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
