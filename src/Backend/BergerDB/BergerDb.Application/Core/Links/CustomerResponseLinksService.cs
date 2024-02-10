using BergerDb.Api.Contracts;
using BergerDb.Application.Core.Abstractions.Links;
using BergerDb.Application.Core.Abstractions.Responses;
using BergerDb.Contracts.Customers.Responses;

namespace BergerDb.Application.Core.Links;

public class CustomerResponseLinksService : ICustomerResponseLinksService
{
    private readonly ILinkService _linkService;

    public CustomerResponseLinksService(ILinkService linkService)
    {
        _linkService = linkService;
    }

    public void GenerateLinks(CustomerResponse response)
    {
        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Customers.GetById,
                new { response.Id },
                "self",
                "GET"));

        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Customers.UpdateAddress,
                new { response.Id },
                "update-customer-address",
                "PUT"));

        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Customers.UpdateCustomer,
                new { response.Id },
                "update-customer-data",
                "PUT"));

        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Customers.UpdateMembership,
                new { response.Id },
                "update-customer-membership",
                "PUT"));

        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Customers.UpdateNotation,
                new { response.Id },
                "update-customer-notation",
                "PUT"));

        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Customers.SendInvoice,
                new { response.Id },
                "send-invoice",
                "POST"));

        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Customers.SendDunning,
                new { response.Id },
                "send-dunning",
                "POST"));

        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Customers.Delete,
                new { response.Id },
                "delete-customer",
                "DELETE"));
    }
}
