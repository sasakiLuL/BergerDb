using BergerDb.Domain.Core.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace BergerDb.Api.Middlewares;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (HttpStatusCode httpStatusCode, ProblemDetails problem) = 
            GetHttpStatusCodeAndProblem(exception);

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = (int)httpStatusCode;

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string response = JsonSerializer.Serialize(problem, serializerOptions);

        await httpContext.Response.WriteAsync(response);

        return true;
    }

    private static (HttpStatusCode, ProblemDetails) GetHttpStatusCodeAndProblem(Exception exception) =>
            exception switch
            {
                BadHttpRequestException =>
                    (HttpStatusCode.BadRequest,
                    new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Bad Request",
                        Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1",
                        Extensions = new Dictionary<string, object?>
                        {
                            { "errors", DomainErrors.General.UnProcessableRequest }
                        }
                    }),
                _ =>
                    (HttpStatusCode.InternalServerError,
                    new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "Internal Server Error",
                        Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1",
                        Extensions = new Dictionary<string, object?>
                        {
                            { "errors", DomainErrors.General.ServerError }
                        }
                    })
            };
}
