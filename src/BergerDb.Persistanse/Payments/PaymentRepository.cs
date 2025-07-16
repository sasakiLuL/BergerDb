using BergerDb.Domain.Payments;

namespace BergerDb.Persistanse.Payments;

public class PaymentRepository : Repository<Payment>, IPaymentsRepository
{
    public PaymentRepository(BergerDbContext context) : base(context)
    {
    }
}
