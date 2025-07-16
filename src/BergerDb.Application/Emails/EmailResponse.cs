using BergerDb.Domain.Emails;

namespace BergerDb.Application.Emails;

public record EmailResponse(
    Guid Id,
    Guid PaymentProcessId,
    EmailType EmailType,
    DateTime SentOnUtc,
    string Subject,
    string From,
    string To,
    string BodyText,
    PdfMetadataResponse PdfMetadata);
