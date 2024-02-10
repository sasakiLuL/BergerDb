using BergerDb.Domain.Core.Primitives;
using FluentValidation;

namespace BergerDb.Domain.Core.Extensions;

public static class ValidatorsExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> ruleBuilder,
        Error failure)
    {
        return ruleBuilder.WithErrorCode(failure.Code).WithMessage(failure.Message);
    }
}
