using AutoMapper;

namespace BergerDb.Domain.Emails.EmailAddresses;

public class NameMapper : Profile
{
    public NameMapper()
    {
        CreateMap<EmailAddress, string>()
            .ConstructUsing(n => n.Value);
    }
}
