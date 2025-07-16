using BergerDb.Domain.Customers;
using BergerDb.UI.Models.Emails;
using BergerDb.UI.Models.Payments;

namespace BergerDb.UI.Models.PaymentProcesses;

public class PaymentProcessModel
{
    public Guid Id { get; set; }

    public PaymentType PaymentType { get; set; }

    public PaymentModel? Payment { get; set; }

    public List<EmailModel> Emails { get; set; } = [];

    public bool IsPending { get; set; }

    public bool IsMade { get; set; }

    public bool IsExpired { get; set; }
}
