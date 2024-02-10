using BergerDb.Domain.Users.Permissions.Enums;

namespace BergerDb.Api.Extensions;

public static class EndpointExtensions
{
    public static RouteGroupBuilder HasPermissions(this RouteGroupBuilder builder, 
        params Permission[] permissions) =>
        builder.RequireAuthorization(permissions.Select(p => p.ToString()).ToArray());

    public static RouteHandlerBuilder HasPermissions(this RouteHandlerBuilder builder,
        params Permission[] permissions) =>
        builder.RequireAuthorization(permissions.Select(p => p.ToString()).ToArray());

    public static IEndpointConventionBuilder HasPermissions(this IEndpointConventionBuilder builder,
        params Permission[] permissions) =>
        builder.RequireAuthorization(permissions.Select(p => p.ToString()).ToArray());}
