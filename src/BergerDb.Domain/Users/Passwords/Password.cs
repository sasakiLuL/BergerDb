using BergerDb.Core.Entities;
using BergerDb.Core.Results;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.Users.Passwords;

public record Password : ValueObject
{
    public static readonly int MinimumLength = 8;

    public static readonly int MaximumLenght = 30;

    public static readonly int HashLenght = 256;

    public static readonly Regex FormatPattern = new(@"^[A-Za-z\d@$!%*#?&]+$");

    public static readonly Regex MissingLetterPattern = new(@"^(?=.*[A-Za-z])");

    public static readonly Regex MissingDigidPattern = new(@"^(?=.*\d)");

    private Password(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Password> Create(string value)
    {
        return Validate(
            new PasswordValidator(),
            new Password(value));
    }
}
