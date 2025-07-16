using AutoMapper;

namespace BergerDb.Domain.Customers.Prefixes;

public class ZipCodeMapper : Profile
{
    public ZipCodeMapper()
    {
        CreateMap<Prefix, string>()
            .ConstructUsing(n => n.Value);
    }
}
