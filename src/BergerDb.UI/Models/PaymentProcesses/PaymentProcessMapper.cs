using AutoMapper;
using BergerDb.Application.PaymentProcesses;

namespace BergerDb.UI.Models.PaymentProcesses;

public class PaymentProcessMapper : Profile
{
    public PaymentProcessMapper()
    {
        CreateMap<PaymentProcessResponse, PaymentProcessModel>()
            .ReverseMap();
    }
}
