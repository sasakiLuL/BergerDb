using BergerDb.Api.Contracts;
using BergerDb.Api.Extensions;
using BergerDb.Application.Users.Login;
using BergerDb.Application.Users.Register;
using BergerDb.Contracts.Users.Requests;
using BergerDb.Domain.Core.Extensions;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BergerDb.Api.Endpoints;

public class AuthenticationEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var authenticationGroup = app.MapGroup(EndpointRoutes.Api)
            .IncludeInOpenApi();

        authenticationGroup
            .MapPost(EndpointRoutes.Authentication.Login, Login)
            .WithName(EndpointNames.Authentication.Login);

        authenticationGroup
            .MapPost(EndpointRoutes.Authentication.Register, Register)
            .WithName(EndpointNames.Authentication.Register);
    }

    public static async Task<IResult> Register(
        [FromBody] RegisterRequest request,
        IValidator<RegisterCommand> validator,
        ISender sender)
    {
        RegisterCommand command = request.Adapt<RegisterCommand>();

        var validationResult = validator.Validate(command).ToResult();

        if (validationResult.IsFailure)
        {
            return validationResult.ToProblemDetails();
        }

        var tokenResult = await sender.Send(command);

        if (tokenResult.IsFailure)
        {
            return tokenResult.ToProblemDetails();
        }

        return Results.Ok(tokenResult.Value);
    }

    public static async Task<IResult> Login(
         [FromBody] LoginRequest request,
         IValidator<LoginCommand> validator,
         ISender sender)
    {
        LoginCommand command = request.Adapt<LoginCommand>();

        var validationResult = validator.Validate(command).ToResult();

        if (validationResult.IsFailure)
        {
            return validationResult.ToProblemDetails();
        }

        var tokenResult = await sender.Send(command);

        if (tokenResult.IsFailure)
        {
            return tokenResult.ToProblemDetails();
        }

        return Results.Ok(tokenResult.Value);
    }
}
