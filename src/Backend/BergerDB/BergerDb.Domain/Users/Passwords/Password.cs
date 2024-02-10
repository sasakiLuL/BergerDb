using BergerDb.Domain.Core.Extensions;
using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Core.Primitives.Result;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.Users.Passwords;

public record Password : ValueObject<string>
{
    public static readonly int MinimumLength = 8;

    public static readonly int MaximumLenght = 30;

    public static readonly int HashLenght = 256;

    public static readonly Regex FormatPattern = new(@"^[A-Za-z\d@$!%*#?&]+$");

    public static readonly Regex MissingLetterPattern = new(@"^(?=.*[A-Za-z])");

    public static readonly Regex MissingDigidPattern = new(@"^(?=.*\d)");

    private Password(string Value) : base(Value)
    {
    }

    public static async Task<Result<Password>> CreateAsync(string password, CancellationToken token = default)
    {
        var passwordInstance = new Password(password);

        return (await new PasswordValidator().ValidateAsync(passwordInstance, token)).ToResult(passwordInstance);
    }
}
