using FluentValidation.Results;
using FluentValidation;
using BergerDb.Shared.Errors;

namespace BergerDb.Shared.Results;

public static class ResultExtensions
{
    public static T Match<T>(this Result result, Func<T> onSuccess, Func<Error[], T> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Errors);
    }

    public static Error ToDomainError(this ValidationFailure failure)
    {
        return new Error(failure.ErrorCode, failure.ErrorMessage);
    }

    public static Result ToDomainResult(this ValidationResult result)
    {
        return result.IsValid ?
            Result.Success() :
            Result.Failure(result.Errors.Select(e => e.ToDomainError()).ToArray());
    }

    public static Result<TValue> ToDomainResult<TValue>(this ValidationResult result, TValue value)
    {
        return result.IsValid ?
            Result.Success(value) :
            Result.Failure<TValue>(result.Errors.Select(e => e.ToDomainError()).ToArray());
    }

    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> ruleBuilder,
        Error failure)
    {
        return ruleBuilder.WithErrorCode(failure.Code).WithMessage(failure.Message);
    }
}