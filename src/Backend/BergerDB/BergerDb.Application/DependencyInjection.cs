using BergerDb.Application.Core.Abstractions.Responses;
using BergerDb.Application.Core.Behaviors;
using BergerDb.Application.Core.Links;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BergerDb.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

            config.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
        });

        services.AddScoped<ICustomerResponseLinksService, CustomerResponseLinksService>();

        services.AddScoped<IUserResponseLinksService, UserResponseLinksService>();

        services.AddScoped<ITokenResponseLinksService, TokenResponseLinksService>();

        return services;
    }
}
