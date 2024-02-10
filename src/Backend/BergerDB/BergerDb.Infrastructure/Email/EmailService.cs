using BergerDb.Application.Core.Abstractions.Email;
using BergerDb.Contracts.Emails.Requests;
using BergerDb.Infrastructure.Email.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BergerDb.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly EmailSettings _mailSettings;

    public EmailService(IOptions<EmailSettings> mailSettings) => _mailSettings = mailSettings.Value;

    public async Task SendEmailAsync(SendEmailRequest request)
    {
        var emailBody = new BodyBuilder();
        emailBody.Attachments.Add(request.FileName, request.PdfFile);
        emailBody.TextBody = request.TextBody;

        var email = new MimeMessage
        {
            From = { new MailboxAddress(_mailSettings.SenderDisplayName, _mailSettings.SenderEmail) },
            To = { MailboxAddress.Parse(request.EmailTo) },
            Subject = request.Subject,
            Body = emailBody.ToMessageBody(),
        };

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.SmtpPort);

        await smtpClient.AuthenticateAsync(_mailSettings.SenderEmail, _mailSettings.SmtpPassword);

        await smtpClient.SendAsync(email);

        await smtpClient.DisconnectAsync(true);
    }
}
