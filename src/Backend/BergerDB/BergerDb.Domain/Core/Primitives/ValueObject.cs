namespace BergerDb.Domain.Core.Primitives;

public abstract record ValueObject<TValue>(TValue Value)
{
    public static implicit operator string(ValueObject<TValue> valueObject)
        => valueObject?.Value?.ToString() ?? string.Empty;
}
