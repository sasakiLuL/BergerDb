using BergerDb.Shared.Entities;
using BergerDb.Shared.Results;

namespace BergerDb.Domain.Customers.Notations;

public record Notation(string Value) : ValueObject
{
    public static readonly int MaximumLength = 3000;

    public static Result<Notation> Create(string value)
    {
        return Validate(
            new NotationValidator(),
            new Notation(value));
    }
}
