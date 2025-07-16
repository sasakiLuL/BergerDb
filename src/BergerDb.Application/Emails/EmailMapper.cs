using AutoMapper;
using BergerDb.Domain.Emails;
using BergerDb.Domain.Emails.PdfMetadatas;

namespace BergerDb.Application.Emails;

public class EmailMapper : Profile
{
    public EmailMapper()
    {
        CreateMap<Email, EmailResponse>()
            .ReverseMap();

        CreateMap<PdfMetadata, PdfMetadataResponse>()
            .ReverseMap();
    }
}
