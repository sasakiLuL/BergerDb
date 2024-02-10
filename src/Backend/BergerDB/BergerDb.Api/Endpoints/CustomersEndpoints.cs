using BergerDb.Api.Contracts;
using BergerDb.Api.Extensions;
using BergerDb.Application.Customers.CreateCustomer;
using BergerDb.Application.Customers.DeleteCustomer;
using BergerDb.Application.Customers.GetCustomer;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Core.Extensions;
using Carter;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BergerDb.Domain.Customers.Memberships;
using BergerDb.Domain.Users.Permissions.Enums;
using BergerDb.Application.Customers.UpdateCustomerAddress;
using BergerDb.Application.Customers.UpdateCustomerMembership;
using BergerDb.Contracts.Customers.Requests;
using BergerDb.Contracts.Customers.Requests.Filtering;
using BergerDb.Application.Customers.UpdateCustomerPersomalInfo;
using BergerDb.Application.Customers.GetCustomers;
using BergerDb.Application.Customers.SendEmailToCustomer;
using BergerDb.Application.Customers.UpdateCustomerNotation;
using BergerDb.Application.Customers.SendDunningToCustomer;

namespace BergerDb.Api.Endpoints;

public class CustomersEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var customerGroup = app.MapGroup(EndpointRoutes.Api)
            .WithOpenApi()
            .RequireAuthorization();

        customerGroup
            .MapGet(EndpointRoutes.Customers.Get, GetCustomers)
            .HasPermissions(Permission.CustomersRead)
            .WithName(EndpointNames.Customers.Get);

        customerGroup
            .MapGet(EndpointRoutes.Customers.GetById, GetCustomer)
            .HasPermissions(Permission.CustomersRead)
            .WithName(EndpointNames.Customers.GetById);

        customerGroup.MapPost(EndpointRoutes.Customers.Create, CreateCustomer)
            .HasPermissions(Permission.CustomersCreate)
            .WithName(EndpointNames.Customers.Create);

        customerGroup.MapDelete(EndpointRoutes.Customers.Delete, DeleteCustomer)
            .HasPermissions(Permission.CustomersDelete)
            .WithName(EndpointNames.Customers.Delete);

        customerGroup.MapPut(EndpointRoutes.Customers.UpdateCustomer, UpdateCustomer)
            .HasPermissions(Permission.CustomersUpdate)
            .WithName(EndpointNames.Customers.UpdateCustomer);

        customerGroup.MapPut(EndpointRoutes.Customers.UpdateAddress, UpdateAddress)
            .HasPermissions(Permission.CustomersUpdate)
            .WithName(EndpointNames.Customers.UpdateAddress);

        customerGroup.MapPut(EndpointRoutes.Customers.UpdateNotation, UpdateNotation)
            .HasPermissions(Permission.CustomersUpdate)
            .WithName(EndpointNames.Customers.UpdateNotation);

        customerGroup.MapPut(EndpointRoutes.Customers.UpdateMembership, UpdateMembership)
            .HasPermissions(Permission.CustomersUpdate)
            .WithName(EndpointNames.Customers.UpdateMembership);

        customerGroup.MapPost(EndpointRoutes.Customers.SendInvoice, SendInvoice)
            .DisableAntiforgery()
            .WithName(EndpointNames.Customers.SendInvoice);

        customerGroup.MapPost(EndpointRoutes.Customers.SendDunning, SendDunning)
            .DisableAntiforgery()
            .WithName(EndpointNames.Customers.SendDunning);
    }

    private async Task<IResult> SendDunning(
        Guid id,
        [FromForm] SendDunningToCustomerRequest request,
        ISender sender)
    {
        using var memoryStream = new MemoryStream();

        request.PdfFile.CopyTo(memoryStream);

        var command = new SendDunningToCustomerCommand(
            id,
            request.Subject,
            request.BodyText,
            request.PdfFile.FileName,
            memoryStream.ToArray());

        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok();
    }

    private async Task<IResult> SendInvoice(
        Guid id,
        [FromForm] SendInvoiceToCustomerRequest request,
        ISender sender)
    {
        using var memoryStream = new MemoryStream();

        request.PdfFile.CopyTo(memoryStream);

        var command = new SendInvoiceToCustomerCommand(
            id,
            request.Subject,
            request.BodyText,
            request.PdfFile.FileName,
            memoryStream.ToArray());

        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok();
    }

    private async Task<IResult> UpdateCustomer(
        Guid id,
        [FromBody] UpdateCustomerPersonalInfoRequest request,
        IValidator<UpdateCustomerPersomalInfoCommand> validator,
        ISender sender)
    {
        var command = new UpdateCustomerPersomalInfoCommand(
            id,
            request.Prefix,
            request.FirstName,
            request.LastName,
            request.Email,
            request.PersonalId,
            (Sex)request.Sex,
            request.RegistrationDate);

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

    private async Task<IResult> UpdateAddress(
        Guid id,
        [FromBody] UpdateCustomerAddressRequest request,
        IValidator<UpdateCustomerAddressCommand> validator,
        ISender sender)
    {
        var command = new UpdateCustomerAddressCommand(
            id,
            request.Street,
            request.ZipCode,
            request.City);

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

    private async Task<IResult> UpdateNotation(
        Guid id,
        [FromBody] UpdateCustomerNotationRequest request,
        IValidator<UpdateCustomerAddressCommand> validator,
        ISender sender)
    {
        var command = new UpdateCustomerNotationCommand(
            id,
            request.Notation);

        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok();
    }

    private async Task<IResult> UpdateMembership(
        Guid id,
        [FromBody] UpdateCustomerMembershipRequest request,
        IValidator<UpdateCustomerMembershipCommand> validator,
        ISender sender)
    {
        var command = new UpdateCustomerMembershipCommand(
            id,
            (PaymentType)request.PaymentType,
            (MemberType)request.MemberType,
            request.Institution,
            (EntryType)request.EntryType,
            request.Amount,
            request.CurrentInvoiceSendedOn,
            request.LastInvoiceSendedOn,
            request.CurrentCreditReceivedOn,
            request.LastCreditReceivedOn,
            request.DunningSendedOn,
            request.TerminatedOn);

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

    public static async Task<IResult> DeleteCustomer(
        Guid id,
        ISender sender)
    {
        var command = new DeleteCustomerCommand(id);

        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.NoContent();
    }

    public static async Task<IResult> CreateCustomer(
        [FromBody] CreateCustomerRequest request,
        IValidator<CreateCustomerCommand> validator,
        ISender sender)
    {
        var command = request.Adapt<CreateCustomerCommand>();

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

    public static async Task<IResult> GetCustomer(
        Guid id,
        ISender sender)
    {
        var query = new GetCustomerQuery(id);

        var result = await sender.Send(query);

        if (result.IsFailure) 
        {
            return result.ToProblemDetails();
        }

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> GetCustomers(
        int page,
        int pageSize,
        bool? isRecivedInvoice,
        bool? isRecivedDunning,
        bool? isDebtor,
        string? sortColumn,
        string? sortOrder,
        string? id,
        string? prefix,
        string? firstName,
        string? lastName,
        string? email,
        long? personalId,
        int? sex,
        DateTime? registrationDateGte,
        DateTime? registrationDateLte,
        string? street,
        string? zipCode,
        string? city,
        int? paymentType,
        int? memberType,
        string? institution,
        int? entryType,
        int? amount,
        DateTime? currentInvoiceSendedOnGte,
        DateTime? currentInvoiceSendedOnLte,
        DateTime? lastInvoiceSendedOnGte,
        DateTime? lastInvoiceSendedOnLte,
        DateTime? currentCreditReceivedOnGte,
        DateTime? currentCreditReceivedOnLte,
        DateTime? lastCreditReceivedOnGte,
        DateTime? lastCreditReceivedOnLte,
        DateTime? dunningSendedOnGte,
        DateTime? dunningSendedOnLte,
        DateTime? terminatedOnGte,
        DateTime? terminatedOnLte,
        ISender sender)
    {
        var query = new GetCustomersQuery(
            new GetCustomersFilters(
                page,
                pageSize,
                isRecivedInvoice,
                isRecivedDunning,
                isDebtor,
                sortColumn,
                sortOrder,
                id,
                prefix,
                firstName,
                lastName,
                email,
                personalId,
                sex,
                registrationDateGte,
                registrationDateLte,
                street,
                zipCode,
                city,
                paymentType,
                memberType,
                institution,
                entryType,
                amount,
                currentInvoiceSendedOnGte,
                currentInvoiceSendedOnLte,
                lastInvoiceSendedOnGte,
                lastInvoiceSendedOnLte,
                currentCreditReceivedOnGte,
                currentCreditReceivedOnLte,
                lastCreditReceivedOnGte,
                lastCreditReceivedOnLte,
                dunningSendedOnGte,
                dunningSendedOnLte,
                terminatedOnGte,
                terminatedOnLte));

        var result = await sender.Send(query);

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }

        return Results.Ok(result.Value);
    }
}
