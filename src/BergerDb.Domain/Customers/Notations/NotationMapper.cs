using AutoMapper;
namespace BergerDb.Domain.Customers.Notations;

public class PrefixMapper : Profile
{
    public PrefixMapper()
    {
        CreateMap<Notation, string>()
            .ConstructUsing(n => n.Value);
    }
}
