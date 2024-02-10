using BergerDb.Application.Core.Abstractions.Messaging;

namespace BergerDb.Application.Customers.SendEmailToCustomer;

public record SendInvoiceToCustomerCommand(
    Guid Id,
    string Subject,
    string BodyText,
    string FileName,
    byte[] FilePdf) : ICommand;
