using BergerDb.Application.Core.Abstractions.Messaging;

namespace BergerDb.Application.Customers.SendDunningToCustomer;

public record SendDunningToCustomerCommand(
    Guid Id,
    string Subject,
    string BodyText,
    string FileName,
    byte[] PdfFile) : ICommand;
