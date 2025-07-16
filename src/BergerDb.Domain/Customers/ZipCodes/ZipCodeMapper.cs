using AutoMapper;
namespace BergerDb.Domain.Customers.ZipCodes;

public class ZipCodeMapper : Profile
{
    public ZipCodeMapper()
    {
        CreateMap<ZipCode, string>()
            .ConstructUsing(n => n.Value);
    }
}
