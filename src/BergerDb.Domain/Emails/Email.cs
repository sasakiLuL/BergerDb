using BergerDb.Domain.Emails.PdfMetadatas;
using BergerDb.Domain.PaymentProcesses;
using BergerDb.Domain.ValueObjects.EmailAddresses;
using BergerDb.Shared.Entities;

namespace BergerDb.Domain.Emails;

public class Email : Entity<EmailId>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Email() : base(new EmailId(Guid.NewGuid())) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Email(
        EmailId id,
        PaymentProcessId paymentProcessId,
        EmailType emailType,
        DateTime sentOnUtc,
        string subject,
        EmailAddress from,
        EmailAddress to,
        string bodyText,
        PdfMetadata pdfMetadata) : base(id)
    {
        PaymentProcessId = paymentProcessId;
        EmailType = emailType;
        SentOnUtc = sentOnUtc;
        Subject = subject;
        From = from;
        To = to;
        BodyText = bodyText;
        PdfMetadata = pdfMetadata;
    }

    public PaymentProcessId PaymentProcessId { get; set; }

    public EmailType EmailType { get; set; }

    public DateTime SentOnUtc { get; set; }

    public string Subject { get; set; }

    public EmailAddress From { get; set; }

    public EmailAddress To { get; set; }

    public string BodyText { get; set; }

    public PdfMetadata PdfMetadata { get; set; }
}
