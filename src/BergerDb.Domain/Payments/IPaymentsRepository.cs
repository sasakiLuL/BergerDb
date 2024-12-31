using BergerDb.Domain.Abstractions;

namespace BergerDb.Domain.Payments;

public interface IPaymentsRepository : IRepository<Payment, PaymentId>
{
}
