using BergerDb.Domain.Users;

namespace BergerDb.Domain.Core.Abstractions.Services;

public interface IPasswordHashChecker
{
    bool HashesMatch(string passwordHash, string providedPassword);
}
