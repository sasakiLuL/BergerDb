using BergerDb.Contracts.Customers.Responses;
using BergerDb.Contracts.Users.Responses;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Users;
using BergerDb.Domain.Users.EmailConfigurations;
using Mapster;

namespace BergerDb.Api.Common;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Customer, CustomerResponse>()
            .MapToConstructor(true)
            .Map(dest => dest.Street, src => src.Address!.Street.Value)
            .Map(dest => dest.ZipCode, src => src.Address!.ZipCode.Value)
            .Map(dest => dest.City, src => src.Address!.City.Value)
            .Map(dest => dest.Amount, src => src.Membership!.Amount)
            .Map(dest => dest.PaymentType, src => src.Membership!.PaymentType)
            .Map(dest => dest.EntryType, src => src.Membership!.EntryType)
            .Map(dest => dest.Institution, src => src.Membership!.Institution.Value)
            .Map(dest => dest.MemberType, src => src.Membership!.MemberType)
            .Map(dest => dest.CurrentInvoiceSendedOn, src => src.Membership!.InvoiceSendedOn.Current)
            .Map(dest => dest.LastInvoiceSendedOn, src => src.Membership!.InvoiceSendedOn.Last)
            .Map(dest => dest.CurrentCreditReceivedOn, src => src.Membership!.CreditReceivedOn.Current)
            .Map(dest => dest.LastCreditReceivedOn, src => src.Membership!.CreditReceivedOn.Last)
            .Map(dest => dest.TerminatedOn, src => src.Membership!.TerminatedOn)
            .Map(dest => dest.Email, src => src.Email.Value)
            .Map(dest => dest.FirstName, src => src.FirstName.Value)
            .Map(dest => dest.LastName, src => src.LastName.Value)
            .Map(dest => dest.Sex, src => src.Sex)
            .Map(dest => dest.PersonalId, src => src.PersonalId)
            .Map(dest => dest.Notation, src => src.Notation.Value)
            .Map(dest => dest.Prefix, src => src.Prefix.Value)
            .Map(dest => dest.DunningSendedOn, src => src.Membership!.DunningSendedOn)
            .Map(dest => dest.IsDebtor, src => src.Membership!.IsDebtor)
            .Map(dest => dest.IsRecivedInvoice, src => src.Membership!.IsRecivedInvoice)
            .Map(dest => dest.IsRecivedDunning, src => src.Membership!.IsRecivedDunning)
            .Ignore(dest => dest.Links);

        config.NewConfig<User, UserResponse>()
            .MapToConstructor(true)
            .Map(dest => dest.UserName, src => src.UserName.Value)
            .Map(dest => dest.Email, src => src.Email.Value)
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.FirstName, src => src.FirstName!.Value)
            .Map(dest => dest.LastName, src => src.LastName!.Value)
            .Ignore(dest => dest.Links);

        config.NewConfig<EmailConfiguration, EmailConfigurationResponse>()
            .MapToConstructor(true)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.ZipCode, src => src.ZipCode)
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.HomePage, src => src.HomePage)
            .Map(dest => dest.AccountName, src => src.AccountName)
            .Map(dest => dest.IBAN, src => src.IBAN)
            .Map(dest => dest.BIC, src => src.BIC)
            .Map(dest => dest.GID, src => src.GID)
            .Map(dest => dest.TaxIdentificationNumber, src => src.TaxIdentificationNumber)
            .Map(dest => dest.InvoicePdfBody, src => src.InvoicePdfBody)
            .Map(dest => dest.InvoiceEmailSubject, src => src.InvoiceEmailSubject)
            .Map(dest => dest.InvoiceEmailBody, src => src.InvoiceEmailBody)
            .Map(dest => dest.BillingRemindingEmailSubject, src => src.BillingRemindingEmailSubject)
            .Map(dest => dest.BillingRemindingEmailBody, src => src.BillingRemindingEmailBody)
            .Map(dest => dest.DirectDebitingRemindingEmailSubject, src => src.DirectDebitingRemindingEmailSubject)
            .Map(dest => dest.DirectDebitingRemindingEmailBody, src => src.DirectDebitingRemindingEmailBody)
            .Ignore(dest => dest.Links);
    }
}
