using BergerDb.Domain.Core.Primitives;

namespace BergerDb.Domain.Core.Primitives.Result;

public class Result<TValue> : Result
{
    private readonly TValue _value;
    internal Result(TValue value, bool isSuccess, params Error[] errors)
        : base(isSuccess, errors)
    {
        _value = value;
    }

    public static implicit operator Result<TValue>(TValue value) => Success(value);

    public TValue Value => IsSuccess ? _value
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");
}
