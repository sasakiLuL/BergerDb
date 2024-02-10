using BergerDb.Application.Core.Abstractions;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Customers.Addresses;
using BergerDb.Domain.Customers.Memberships;
using BergerDb.Domain.Users;
using BergerDb.Domain.Users.EmailConfigurations;
using BergerDb.Domain.Users.Roles;
using BergerDb.Persistence.Addresses;
using BergerDb.Persistence.Customers;
using BergerDb.Persistence.EmailConfigurations;
using BergerDb.Persistence.Memberships;
using BergerDb.Persistence.Roles;
using BergerDb.Persistence.Services;
using BergerDb.Persistence.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BergerDb.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("BergerDb") ??
            throw new ArgumentNullException("Wrong connection string.");

        services.AddDbContextPool<BergerDbContext>(options =>
            options.UseNpgsql(connectionString, options =>
            {
                options.EnableRetryOnFailure();
            }));

        services.AddScoped<IAddressRepository, AddressRepository>();

        services.AddScoped<IMembershipRepository, MembershipRepository>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IEmailConfigurationRepository, EmailConfigurationRepository>();

        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IDbContextService, DbContextService>();

        return services;
    }
}
