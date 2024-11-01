namespace BergerDb.Infrastructure.Authorization;

public class AuthenticationOptions
{
    public string Type { get; set; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string Secret { get; set; } = string.Empty;

    public int ExpiresInMinutes { get; set; }

    public static readonly string Section = "Authentication";
}
