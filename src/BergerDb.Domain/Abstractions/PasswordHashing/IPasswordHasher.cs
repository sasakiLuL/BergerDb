namespace BergerDb.Domain.Abstractions.PasswordHashing;

public interface IPasswordHasher
{
    string Hash(string password);

    bool Verify(string passwordHash, string providedPassword);
}
