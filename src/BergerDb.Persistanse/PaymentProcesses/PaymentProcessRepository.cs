using BergerDb.Domain.PaymentProcesses;

namespace BergerDb.Persistanse.PaymentProcesses;

public class PaymentProcessRepository : Repository<PaymentProcess, PaymentProcessId>, IPaymentProcessRepository
{
    public PaymentProcessRepository(BergerDbContext context) : base(context)
    {
    }
}
