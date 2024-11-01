using BergerDb.Core.Results;
using FluentValidation;

namespace BergerDb.Core.Entities;

public record ValueObject
{
    protected static Result<TValue> Validate<TValue>(
        AbstractValidator<TValue> validator,
        TValue instance)
    {
        return validator
            .Validate(instance)
            .ToResult(instance);
    }
}
