namespace BergerDb.Application.Payments;

public record PaymentResponse(
    Guid Id,
    decimal Value,
    DateTime PayedOnUtc);
