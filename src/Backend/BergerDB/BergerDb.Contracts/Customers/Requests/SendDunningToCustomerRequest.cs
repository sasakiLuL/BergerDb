using Microsoft.AspNetCore.Http;

namespace BergerDb.Contracts.Customers.Requests;

public record SendDunningToCustomerRequest(
    IFormFile PdfFile,
    string Subject,
    string BodyText,
    string FileName);
