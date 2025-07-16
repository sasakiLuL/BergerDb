using AutoMapper;
using BergerDb.Domain.PaymentProcesses;

namespace BergerDb.Application.PaymentProcesses;

public class PaymentProcessMapper : Profile
{
    public PaymentProcessMapper()
    {
        CreateMap<PaymentProcess, PaymentProcess>()
            .ReverseMap();
    }
}
