namespace BergerDb.Domain.Core.Primitives;

public record Error(string Code, string Message)
{
    public static implicit operator string(Error error) => error?.Code ?? string.Empty;
}
