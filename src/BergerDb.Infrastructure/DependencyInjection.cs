using BergerDb.Application.Abstractions.Authorization;
using BergerDb.Domain.Abstractions.PasswordHashing;
using BergerDb.Infrastructure.Authorization;
using BergerDb.Infrastructure.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BergerDb.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationOptions = configuration.GetSection(AuthenticationOptions.Section).Get<AuthenticationOptions>() ??
            throw new ArgumentException("Secret jwt key was not provided.");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(authenticationOptions.Secret)),
                    ValidIssuer = authenticationOptions.Issuer,
                    ValidAudience = authenticationOptions.Audience,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();

        services.Configure<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.Section));

        services.AddSingleton<ITokenResponseProvider, TokenResponseProvider>();

        services.AddSingleton<IPasswordHasher, PaswordHasher>();

        return services;
    }
}
