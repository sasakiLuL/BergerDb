using BergerDb.Domain.Abstractions.PasswordHashing;
using System.Security.Cryptography;

namespace BergerDb.Infrastructure.Cryptography;

public class PaswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;

    private const int HashSize = 32;

    private const int Iterations = 100000;

    private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password, salt, Iterations, Algorithm, HashSize);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool Verify(string passwordHash, string providedPassword)
    {
        string[] partrs = passwordHash.Split('-');

        byte[] hash = Convert.FromHexString(partrs[0]);

        byte[] salt = Convert.FromHexString(partrs[1]);

        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
            providedPassword, salt, Iterations, Algorithm, HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}
