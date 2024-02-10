namespace BergerDb.Domain.Core.Primitives.Result;

public class Result
{
    protected Result(bool isSuccess, params Error[] errors)
    {
        if (isSuccess && errors.Length != 0)
        {
            Console.WriteLine();
            throw new InvalidOperationException();
        }

        if (!isSuccess && errors.Length == 0)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Errors = errors;
    }

    public Error[] Errors { get; private set; }

    public bool IsSuccess { get; private set; }

    public bool IsFailure { get => !IsSuccess; }

    public static Result Success() => new(true, []);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, []);

    public static Result Failure(params Error[] error) => new(false, error);

    public static Result<TValue> Failure<TValue>(params Error[] error) => new(default!, false, error);

    public static Result Concat(params Result[] results)
    {
        var failures = results.Where(e => e.IsFailure).Select(e => e.Errors);

        if (failures.Count() == 0)
        {
            return Success();
        }

        return Failure([.. failures.SelectMany(e => e)]);
    }

    public Dictionary<string, string[]> ToDictionary()
    {
        if (IsSuccess)
        {
            throw new InvalidOperationException();
        }

        Dictionary<string, string[]> dict = new();

        foreach (var error in Errors)
        {
            dict.Add(error.Code, [error.Message]);
        }

        return dict;
    }
}
