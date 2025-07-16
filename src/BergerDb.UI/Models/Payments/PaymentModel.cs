namespace BergerDb.UI.Models.Payments;

public class PaymentModel
{
    public PaymentModel(
        Guid id, 
        decimal value, 
        DateTime payedOnUtc)
    {
        Id = id;
        Value = value;
        PayedOnUtc = payedOnUtc;
    }

    public Guid Id { get; set; }

    public decimal Value { get; set; }

    public DateTime PayedOnUtc { get; set; }
}
