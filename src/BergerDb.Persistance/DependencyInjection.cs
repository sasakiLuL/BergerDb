using BergerDb.Domain.Abstractions.Repositories;
using BergerDb.Domain.Users;
using BergerDb.Persistance.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BergerDb.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BergerDbContext>(options =>
        {
            string connectionString = configuration.GetConnectionString("Database") ?? 
                throw new ArgumentException("The connection string is not provided.");

            options.UseNpgsql(connectionString, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
