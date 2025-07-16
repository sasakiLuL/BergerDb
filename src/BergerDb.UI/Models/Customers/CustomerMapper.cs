using AutoMapper;
using BergerDb.Application.Customers;

namespace BergerDb.UI.Models.Customers;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<CustomerResponse, CustomerModel>()
            .ReverseMap();
    }
}
