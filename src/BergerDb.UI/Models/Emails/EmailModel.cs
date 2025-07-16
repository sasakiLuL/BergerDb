using BergerDb.Domain.Emails;
using BergerDb.UI.Models.Emails.PdfMetadatas;

namespace BergerDb.UI.Models.Emails;

public class EmailModel
{
    public Guid Id { get; set; }

    public Guid PaymentProcessId { get; set; }

    public EmailType EmailType { get; set; }

    public DateTime SentOnUtc { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string From { get; set; } = string.Empty;

    public string To { get; set; } = string.Empty;

    public string BodyText { get; set; } = string.Empty;

    public PdfMetadataModel PdfMetadata { get; set; }
}
