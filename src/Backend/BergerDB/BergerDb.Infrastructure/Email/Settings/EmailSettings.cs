namespace BergerDb.Infrastructure.Email.Settings;

public class EmailSettings
{
    public EmailSettings()
    {
        SenderDisplayName = string.Empty;
        SenderEmail = string.Empty;
        SmtpPassword = string.Empty;
        SmtpServer = string.Empty;
        SmtpPort = 0;
    }

    public string SenderDisplayName { get; set; }

    public string SenderEmail { get; set; }

    public string SmtpPassword { get; set; }

    public string SmtpServer { get; set; }

    public int SmtpPort { get; set; }


    public const string SettingsKey = "Mail";
}
