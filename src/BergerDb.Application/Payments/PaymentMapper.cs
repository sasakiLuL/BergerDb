using AutoMapper;
using BergerDb.Domain.Payments;

namespace BergerDb.Application.Payments;

public class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<Payment, PaymentResponse>()
            .ReverseMap();
    }
}
