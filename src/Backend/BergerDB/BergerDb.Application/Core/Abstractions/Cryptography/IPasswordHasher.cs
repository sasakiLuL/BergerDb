using BergerDb.Domain.Users.Passwords;

namespace BergerDb.Application.Core.Abstractions.Cryptography;

public interface IPasswordHasher
{
    string HashPassword(Password password);
}
