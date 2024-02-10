using BergerDb.Contracts.Emails.Requests;

namespace BergerDb.Application.Core.Abstractions.Email;

public interface IEmailService
{
    Task SendEmailAsync(SendEmailRequest request);
}
