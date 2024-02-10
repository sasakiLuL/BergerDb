namespace BergerDb.Contracts.Emails.Requests;

public record SendEmailRequest(
    string EmailTo,
    string Subject,
    string TextBody,
    string FileName,
    byte[] PdfFile);

