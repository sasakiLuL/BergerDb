using BergerDb.Shared.Entities;
using BergerDb.Shared.Results;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.ValueObjects.EmailAddresses;

public record EmailAddress : ValueObject
{
    public static readonly int MaximumLength = 256;

    public static readonly Regex FormatPattern = EmailAddressRegex.GetRegex();

    private EmailAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<EmailAddress> Create(string value)
    {
        return Validate(
            new EmailAddressValidator(),
            new EmailAddress(value));
    }
}
