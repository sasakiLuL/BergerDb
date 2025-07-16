using BergerDb.Application.Emails;
using BergerDb.Application.Payments;
using BergerDb.Domain.Customers;

namespace BergerDb.Application.PaymentProcesses;

public record PaymentProcessResponse(
    Guid Id,
    PaymentType PaymentType,
    PaymentResponse? Payment,
    List<EmailResponse> Emails,
    bool IsPending,
    bool IsMade,
    bool IsExpired);