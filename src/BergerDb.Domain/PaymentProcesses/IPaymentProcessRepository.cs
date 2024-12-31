using BergerDb.Domain.Abstractions;

namespace BergerDb.Domain.PaymentProcesses;

public interface IPaymentProcessRepository : IRepository<PaymentProcess, PaymentProcessId>
{
}
