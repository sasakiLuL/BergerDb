using AutoMapper;

namespace BergerDb.Domain.Customers.Names;

public class NameMapper : Profile
{
    public NameMapper()
    {
        CreateMap<Name, string>()
            .ConstructUsing(n => n.Value);
    }
}
