using BergerDb.Application.Core.Abstractions;
using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Application.Core.Abstractions.Responses;
using BergerDb.Contracts.Common;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Mapster;
using BergerDb.Domain.Customers.Memberships;
using BergerDb.Contracts.Customers.Responses;
using BergerDb.Domain.Core.Abstractions;

namespace BergerDb.Application.Customers.GetCustomers;

public class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, PagedList<CustomerResponse>>
{
    private readonly IDbContextService _dbContextService;

    private readonly ICustomerResponseLinksService _customerResponseLinkService;

    private readonly IUnitOfWork _unitOfWork;

    private readonly ICustomerRepository _customerRepository;

    public GetCustomersQueryHandler(
        IDbContextService dbContextService, 
        ICustomerResponseLinksService responseLinkService, 
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository)
    {
        _dbContextService = dbContextService;
        _customerResponseLinkService = responseLinkService;
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
    }

    public async Task<Result<PagedList<CustomerResponse>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetCustomersAsync();

        customers.ToList().ForEach(customer => customer.Membership!.UpdateStatus());

        await _unitOfWork.SaveChangesAsync();

        var customerQuery = _dbContextService.GetQuaryable<Customer>();        

        if (!string.IsNullOrEmpty(request.Filters.Id))
        {
            customerQuery = customerQuery.Where(c =>
                c.Id.ToString().ToLower().Contains(request.Filters.Id.ToLower()));
        }

        if (request.Filters.IsDebtor is not null && request.Filters.IsDebtor.Value)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.IsDebtor);
        }

        if (request.Filters.IsRecivedInvoice is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.IsRecivedInvoice == request.Filters.IsRecivedInvoice);
        }

        if (request.Filters.IsRecivedDunning is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.IsRecivedDunning == request.Filters.IsRecivedDunning);
        }

        if (!string.IsNullOrEmpty(request.Filters.FirstName))
        {
            customerQuery = customerQuery.Where(c =>
                c.FirstName.Value.ToLower().Contains(request.Filters.FirstName.ToLower()));
        }

        if (!string.IsNullOrEmpty(request.Filters.Prefix))
        {
            customerQuery = customerQuery.Where(c =>
                c.FirstName.Value.ToLower().Contains(request.Filters.Prefix.ToLower()));
        }

        if (!string.IsNullOrEmpty(request.Filters.LastName))
        {
            customerQuery = customerQuery.Where(c =>
                c.LastName.Value.ToLower().Contains(request.Filters.LastName.ToLower()));
        }

        if (!string.IsNullOrEmpty(request.Filters.Email))
        {
            customerQuery = customerQuery.Where(c =>
                c.Email.Value.ToLower().Contains(request.Filters.Email.ToLower()));
        }

        if (request.Filters.Sex is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Sex == (Sex)request.Filters.Sex);
        }

        if (request.Filters.RegistrationDateGte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.RegistrationDate.ToLocalTime().Month >= request.Filters.RegistrationDateGte.Value.Month);
        }

        if (request.Filters.RegistrationDateLte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.RegistrationDate.ToLocalTime().Month <= request.Filters.RegistrationDateLte.Value.Month);
        }

        if (!string.IsNullOrEmpty(request.Filters.Street))
        {
            customerQuery = customerQuery.Where(c =>
                c.Address!.Street.Value.ToLower().Contains(request.Filters.Street.ToLower()));
        }

        if (!string.IsNullOrEmpty(request.Filters.ZipCode))
        {
            customerQuery = customerQuery.Where(c =>
                c.Address!.ZipCode.Value.ToLower().Contains(request.Filters.ZipCode.ToLower()));
        }

        if (!string.IsNullOrEmpty(request.Filters.City))
        {
            customerQuery = customerQuery.Where(c =>
                c.Address!.City.Value.ToLower().Contains(request.Filters.City.ToLower()));
        }

        if (request.Filters.MemberType is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.MemberType == (MemberType)request.Filters.MemberType);
        }

        if (!string.IsNullOrEmpty(request.Filters.Institution))
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.Institution.Value.ToLower().Contains(request.Filters.Institution.ToLower()));
        }

        if (request.Filters.EntryType is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.EntryType == (EntryType)request.Filters.EntryType);
        }

        if (request.Filters.PaymentType is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.PaymentType == (PaymentType)request.Filters.PaymentType);
        }

        if (request.Filters.Amount is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.Amount == request.Filters.Amount);
        }

        if (request.Filters.CurrentInvoiceSendedOnGte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.InvoiceSendedOn.Current >= request.Filters.CurrentInvoiceSendedOnGte.Value.ToUniversalTime());
        }

        if (request.Filters.CurrentInvoiceSendedOnLte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.InvoiceSendedOn.Current <= request.Filters.CurrentInvoiceSendedOnLte.Value.ToUniversalTime());
        }

        if (request.Filters.LastInvoiceSendedOnGte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.InvoiceSendedOn.Last >= request.Filters.LastInvoiceSendedOnGte.Value.ToUniversalTime());
        }

        if (request.Filters.LastInvoiceSendedOnLte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.InvoiceSendedOn.Last <= request.Filters.LastInvoiceSendedOnLte.Value.ToUniversalTime());
        }

        if (request.Filters.CurrentCreditReceivedOnGte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.CreditReceivedOn.Current >= request.Filters.CurrentCreditReceivedOnGte.Value.ToUniversalTime());
        }

        if (request.Filters.CurrentCreditReceivedOnLte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.CreditReceivedOn.Current <= request.Filters.CurrentCreditReceivedOnLte.Value.ToUniversalTime());
        }

        if (request.Filters.LastCreditReceivedOnGte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.CreditReceivedOn.Last >= request.Filters.LastCreditReceivedOnGte.Value.ToUniversalTime());
        }

        if (request.Filters.LastCreditReceivedOnLte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.CreditReceivedOn.Last <= request.Filters.LastCreditReceivedOnLte.Value.ToUniversalTime());
        }

        if (request.Filters.TerminatedOnGte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.TerminatedOn >= request.Filters.TerminatedOnGte.Value.ToUniversalTime());
        }

        if (request.Filters.TerminatedOnLte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.TerminatedOn <= request.Filters.TerminatedOnLte.Value.ToUniversalTime());
        }

        if (request.Filters.DunningSendedOnGte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.DunningSendedOn >= request.Filters.DunningSendedOnGte.Value.ToUniversalTime());
        }

        if (request.Filters.DunningSendedOnLte is not null)
        {
            customerQuery = customerQuery.Where(c =>
                c.Membership!.DunningSendedOn <= request.Filters.DunningSendedOnLte.Value.ToUniversalTime());
        }

        var sortColumn = GetSortColumn(request);

        if (request.Filters.SortOrder?.ToLower() == "desc")
        {
            customerQuery = customerQuery.OrderByDescending(sortColumn);
        }
        else
        {
            customerQuery = customerQuery.OrderBy(sortColumn);
        }

        var customersResponsesQuery = customerQuery
            .Include(c => c.Address)
            .Include(c => c.Membership)
            .Select(c => c.Adapt<CustomerResponse>());

        var filteredCustomers = await PagedList<CustomerResponse>.CreateAsync(
            customersResponsesQuery,
            request.Filters.Page,
            request.Filters.PageSize,
            cancellationToken);

        filteredCustomers.Items.ForEach(c => _customerResponseLinkService.GenerateLinks(c));

        return Result.Success(filteredCustomers);
    }

    private Expression<Func<Customer, object>> GetSortColumn(GetCustomersQuery request) =>
        request.Filters.SortColumn?.ToLower() switch
        {
            "prefix" => customer => customer.Prefix.Value,
            "firstname" => customer => customer.FirstName.Value,
            "lastname" => customer => customer.LastName.Value,
            "email" => customer => customer.Email.Value,
            "notation" => customer => customer.Notation,
            "personalid" => customer => customer.PersonalId,
            "sex" => customer => customer.Sex,
            "registrationdate" => customer => customer.RegistrationDate.Month,
            "street" => customer => customer.Address!.Street.Value,
            "zipcode" => customer => customer.Address!.ZipCode.Value,
            "city" => customer => customer.Address!.City.Value,
            "membertype" => customer => customer.Membership!.MemberType,
            "institution" => customer => customer.Membership!.Institution.Value,
            "entrytype" => customer => customer.Membership!.EntryType,
            "paymenttype" => customer => customer.Membership!.PaymentType,
            "amount" => customer => customer.Membership!.Amount,
            "currentinvoicesendedon" => customer => customer.Membership!.InvoiceSendedOn.Current!,
            "lastinvoicesendedon" => customer => customer.Membership!.InvoiceSendedOn.Last!,
            "currentcreditreceivedon" => customer => customer.Membership!.CreditReceivedOn.Current!,
            "lastcreditreceivedon" => customer => customer.Membership!.CreditReceivedOn.Last!,
            "dunningsendedon" => customer => customer.Membership!.DunningSendedOn!,
            "terminatedon" => customer => customer.Membership!.TerminatedOn!,
            _ => customer => customer.Id
        };
}
