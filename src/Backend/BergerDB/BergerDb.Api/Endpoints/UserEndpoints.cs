using BergerDb.Api.Contracts;
using BergerDb.Api.Extensions;
using BergerDb.Application.Users.ChangePassword;
using BergerDb.Application.Users.DeleteUser;
using BergerDb.Application.Users.GetUser;
using BergerDb.Application.Users.GetUsers;
using BergerDb.Application.Users.UpdateUser;
using BergerDb.Domain.Core.Extensions;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BergerDb.Application.Users.GetEmailConfiguration;
using BergerDb.Application.Users.UpdateEmailConfiguration;
using BergerDb.Domain.Users.Permissions.Enums;
using BergerDb.Contracts.Users.Requests;

namespace BergerDb.Api.Endpoints;

public class UserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var userGroup = app.MapGroup(EndpointRoutes.Api)
            .WithOpenApi()
            .RequireAuthorization();

        userGroup.MapGet(EndpointRoutes.Users.Get, GetUsers)
            .HasPermissions(Permission.UsersRead)
            .WithName(EndpointNames.Users.Get);

        userGroup.MapGet(EndpointRoutes.Users.GetById, GetUser)
            .HasPermissions(Permission.UsersRead)
            .WithName(EndpointNames.Users.GetById);

        userGroup.MapPut(EndpointRoutes.Users.Update, UpdateUser)
            .HasPermissions(Permission.UsersUpdate)
            .WithName(EndpointNames.Users.Update);

        userGroup.MapPut(EndpointRoutes.Users.ChangePassword, ChangePassword)
            .HasPermissions(Permission.UsersUpdate)
            .WithName(EndpointNames.Users.ChangePassword);

        userGroup.MapDelete(EndpointRoutes.Users.Delete, DeleteUser)
            .HasPermissions(Permission.UsersDelete)
            .WithName(EndpointNames.Users.Delete);

        userGroup.MapGet(EndpointRoutes.Users.GetEmailConfiguration, GetEmailConfiguration)
            .HasPermissions(Permission.UsersRead)
            .WithName(EndpointNames.Users.GetEmailConfiguration);

        userGroup.MapPut(EndpointRoutes.Users.UpdateEmailConfiguration, UpdateEmailConfiguration)
            .HasPermissions(Permission.UsersUpdate)
            .WithName(EndpointNames.Users.UpdateEmailConfiguration);
    }

    public static async Task<IResult> ChangePassword(
        Guid id,
        [FromBody] ChangePasswordRequest request,
        IValidator<ChangePasswordCommand> validator,
        ISender sender)
    {
        var command = new ChangePasswordCommand(
            id, request.Password);

        var validationResult = validator.Validate(command).ToResult();

        if (validationResult.IsFailure) 
        {
            return validationResult.ToProblemDetails();
        }

        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok();
    }

    public static async Task<IResult> DeleteUser(
        Guid id,
        ISender sender)
    {
        var command = new DeleteUserCommand(id);

        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.NoContent();
    }

    public static async Task<IResult> UpdateUser(
        Guid id,
        [FromBody] UpdateUserRequest request,
        IValidator<UpdateUserCommand> validator,
        ISender sender)
    {
        var command = new UpdateUserCommand(id, 
            request.UserName,
            request.FirstName,
            request.LastName);

        var validationResult = validator.Validate(command).ToResult();

        if (validationResult.IsFailure) 
        {
            return validationResult.ToProblemDetails();
        }

        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok();
    }

    public static async Task<IResult> GetUser(
        Guid id,
        ISender sender)
    {
        var query = new GetUserQuery(id);

        var result = await sender.Send(query);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> GetUsers(
        string? sortTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        ISender sender)
    {
        var command = new GetUsersQuery(sortTerm, sortColumn, sortOrder, page, pageSize);

        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> GetEmailConfiguration(
        Guid id,
        ISender sender)
    {
        var query = new GetEmailConfigurationQuery(id);

        var result = await sender.Send(query);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> UpdateEmailConfiguration(
        Guid id,
        [FromBody] UpdateEmailConfigurationRequest request,
        ISender sender)
    {
        var command = new UpdateEmailConfigurationCommand(
            id,
            request.Street,
            request.City,
            request.ZipCode,
            request.PhoneNumber,
            request.Email,
            request.HomePage,
            request.AccountName,
            request.IBAN,
            request.BIC,
            request.GID,
            request.TaxIdentificationNumber,
            request.InvoicePdfBody,
            request.InvoiceEmailSubject,
            request.InvoiceEmailBody,
            request.BillingRemindingEmailSubject,
            request.BillingRemindingEmailBody,
            request.DirectDebitingRemindingEmailSubject,
            request.DirectDebitingRemindingEmailBody);

        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok();
    }
}
