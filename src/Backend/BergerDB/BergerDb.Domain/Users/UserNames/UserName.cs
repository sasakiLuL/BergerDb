using BergerDb.Domain.Core.Extensions;
using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Core.Primitives.Result;

namespace BergerDb.Domain.Users.UserNames;

public record UserName : ValueObject<string>
{
    public static readonly int MinimumLength = 4;

    public static readonly int MaximumLength = 30;

    private UserName(string Value) : base(Value) { }

    public static async Task<Result<UserName>> CreateAsync(string userName, CancellationToken token = default)
    {
        var userNameInstance = new UserName(userName);

        return (await new UserNameValidator().ValidateAsync(userNameInstance, token))
            .ToResult(userNameInstance);
    }
}
