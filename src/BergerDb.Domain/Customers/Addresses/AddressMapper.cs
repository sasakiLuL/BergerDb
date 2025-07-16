using AutoMapper;

namespace BergerDb.Domain.Customers.Addresses;

public class NotationMapper : Profile
{
    public NotationMapper()
    {
        CreateMap<Address, string>()
            .ConstructUsing(n => n.Value);
    }
}
