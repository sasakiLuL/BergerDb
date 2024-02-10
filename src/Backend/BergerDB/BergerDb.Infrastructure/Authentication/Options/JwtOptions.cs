namespace BergerDb.Infrastructure.Authentication.Options;

public class JwtOptions
{
    public const string SettingsKey = "Jwt";

    public JwtOptions()
    {
        Issuer = string.Empty;
        Audience = string.Empty;
        SecretKey = string.Empty;
    }

    public string Issuer { get; set; }

    public string Audience { get; set; }

    public string SecretKey { get; set; }
}