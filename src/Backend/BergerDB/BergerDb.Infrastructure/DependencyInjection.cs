using BergerDb.Application.Core.Abstractions.Authorization;
using BergerDb.Application.Core.Abstractions.Cryptography;
using BergerDb.Application.Core.Abstractions.Email;
using BergerDb.Application.Core.Abstractions.Links;
using BergerDb.Application.Core.Abstractions.Responses;
using BergerDb.Application.Customers.GetCustomer;
using BergerDb.Contracts.Customers;
using BergerDb.Domain.Core.Abstractions.Services;
using BergerDb.Infrastructure.Authentication;
using BergerDb.Infrastructure.Authentication.Options;
using BergerDb.Infrastructure.Cryptography;
using BergerDb.Infrastructure.Email;
using BergerDb.Infrastructure.Email.Settings;
using BergerDb.Infrastructure.Links;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BergerDb.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
                };
            });

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SettingsKey));

        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SettingsKey));

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IPasswordHashChecker, PasswordHasher>();

        services.AddAuthorization();

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandle>();

        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddTransient<IEmailService, EmailService>();

        services.AddScoped<IPermissionService, PermissionService>();

        services.AddHttpContextAccessor();

        services.AddScoped<IUserIdetifierProvider, UserIdentifierProvider>();

        services.AddScoped<ILinkService, LinkService>();

        return services;
    }
}
