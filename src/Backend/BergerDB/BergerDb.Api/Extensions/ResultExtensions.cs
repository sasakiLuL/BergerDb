using BergerDb.Domain.Core.Primitives.Result;

namespace BergerDb.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        return Results.Problem(
            statusCode: StatusCodes.Status400BadRequest,
            title: "Bad Request",
            type: "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1",
            extensions: new Dictionary<string, object?>
            {
                { "errors", result.Errors }
            });
    }
}
