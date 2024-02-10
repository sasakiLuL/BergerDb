using BergerDb.Domain.Core.Extensions;
using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Core.Primitives.Result;

namespace BergerDb.Domain.Customers.Notations;

public record Notation : ValueObject<string>
{
    public static readonly int MaximumLength = 3000;

    public Notation(string Value) : base(Value) { }

    public static async Task<Result<Notation>> CreateAsync(string notation, CancellationToken token = default)
    {
        var notationInstance = new Notation(notation);

        return (await new NotationValidator().ValidateAsync(notationInstance, token))
            .ToResult(notationInstance);
    }
}
