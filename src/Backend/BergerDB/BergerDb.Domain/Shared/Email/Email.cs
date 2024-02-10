using BergerDb.Domain.Core.Extensions;
using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Core.Primitives.Result;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.Shared.Email;

public record Email : ValueObject<string>
{
    public static readonly int MaximumLength = 256;

    public static readonly Regex FormatPattern = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

    private Email(string Value) : base(Value) {}

    public static async Task<Result<Email>> CreateAsync(string email, CancellationToken token = default)
    {
        var emailInstance = new Email(email);

        return (await new EmailValidator().ValidateAsync(emailInstance, token))
            .ToResult(emailInstance);
    }
}
