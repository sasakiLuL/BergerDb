using BergerDb.Core.Entities;
using BergerDb.Core.Results;
using BergerDb.Domain.Abstractions.PasswordHashing;
using BergerDb.Domain.Users.Passwords;

namespace BergerDb.Domain.Users;

public class User(UserModel model) : Entity<UserModel>(model)
{
    public Result ChangePassword(Password password, IPasswordHasher passwordHasher)
    {
        var passwordHash = passwordHasher.Hash(password.Value);

        if (passwordHash == Model.PasswordHash)
        {
            return Result.Failure(UserErrors.CannotChangePassword);
        }

        Model.PasswordHash = passwordHash;

        return Result.Success();
    }
}
