using AutoMapper;
using BergerDb.Domain.Customers;

namespace BergerDb.Application.Customers;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<Customer, CustomerResponse>()
            .ReverseMap();
    }
}
