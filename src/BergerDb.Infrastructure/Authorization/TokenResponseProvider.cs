using BergerDb.Application.Abstractions.Authorization;
using BergerDb.Application.Users;
using BergerDb.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace BergerDb.Infrastructure.Authorization;

public class TokenResponseProvider(IOptions<AuthenticationOptions> options) : ITokenResponseProvider
{
    public TokenResponse Create(User user)
    {
        var secretKey = options.Value.Secret;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var expirationTime = DateTime.UtcNow.AddMinutes(options.Value.ExpiresInMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, user.Model.Id.Value.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Model.Email.Value)]),
            Expires = expirationTime,
            SigningCredentials = credentials,
            Issuer = options.Value.Issuer,
            Audience = options.Value.Audience,
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return new TokenResponse(options.Value.Type, token, new DateTimeOffset(expirationTime).ToUnixTimeSeconds());
    }
}
