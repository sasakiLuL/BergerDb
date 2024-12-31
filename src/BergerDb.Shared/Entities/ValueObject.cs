using BergerDb.Shared.Results;
using FluentValidation;

namespace BergerDb.Shared.Entities;

public record ValueObject
{
    protected static Result<TValue> Validate<TValue>(
        AbstractValidator<TValue> validator,
        TValue instance)
    {
        return validator
            .Validate(instance)
            .ToDomainResult(instance);
    }
}
