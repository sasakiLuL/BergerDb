using BergerDb.Domain.Core.Extensions;
using BergerDb.Domain.Core.Primitives.Result;

namespace BergerDb.Domain.Shared.DateRanges;

public class InvoiceDateRange
{
    private InvoiceDateRange(DateTime? current, DateTime? last)
    {
        Current = current;
        Last = last;
    }

    public DateTime? Current { get; private set; }

    public DateTime? Last { get; private set; }

    public static async Task<Result<InvoiceDateRange>> CreateAsync(DateTime? current = null, DateTime? last = null, CancellationToken token = default)
    {
        var dateRangeInstanse = new InvoiceDateRange(current, last);

        return (await new InvoiceDateRangeValidator().ValidateAsync(dateRangeInstanse, token))
            .ToResult(dateRangeInstanse);
    }
}
