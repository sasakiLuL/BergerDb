using BergerDb.Application.Core.Abstractions.Authorization;
using BergerDb.Domain.Users;
using BergerDb.Infrastructure.Authentication.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BergerDb.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;

    private readonly IPermissionService _permissionService;

    public JwtProvider(
        IOptions<JwtOptions> jwtOptions,
        IPermissionService permissionService)
    {
        _jwtOptions = jwtOptions.Value;
        _permissionService = permissionService;
    }

    public async Task<string> CreateAsync(User user)
    {
        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email.Value),
            new(JwtRegisteredClaimNames.Name, user.UserName.Value),
        ];

        HashSet<string> permissions = await _permissionService.GetPermissionsAsync(user.Id);

        foreach (string permission in permissions)
        {
            claims.Add(new(CustomClaims.Permissions, permission));
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(6),
            signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}
