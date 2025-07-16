using AutoMapper;
using BergerDb.Application.Emails;
using BergerDb.UI.Models.Emails.PdfMetadatas;

namespace BergerDb.UI.Models.Emails;

public class EmailMapper : Profile
{
    public EmailMapper()
    {
        CreateMap<EmailResponse, EmailModel>()
            .ReverseMap();

        CreateMap<PdfMetadataResponse, PdfMetadataModel>()
            .ReverseMap();
    }
}
