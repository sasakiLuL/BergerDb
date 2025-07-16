using AutoMapper;
using BergerDb.Application.Payments;

namespace BergerDb.UI.Models.Payments;

public class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<PaymentResponse, PaymentModel>()
            .ReverseMap();
    }
}
