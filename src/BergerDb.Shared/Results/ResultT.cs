﻿using BergerDb.Shared.Errors;

namespace BergerDb.Shared.Results;

public class Result<TValue> : Result
{
    private readonly TValue _value;

    protected internal Result(TValue value, bool isSuccess, Error[] errors)
            : base(isSuccess, errors)
            => _value = value;

    public static implicit operator Result<TValue>(TValue value) => Success(value);

    public TValue Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");
}
