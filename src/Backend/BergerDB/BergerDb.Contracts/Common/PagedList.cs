using Microsoft.EntityFrameworkCore;

namespace BergerDb.Contracts.Common;

public record PagedList<TResponse>
{
    private PagedList(
        List<TResponse> Items,
        int Page,
        int PageSize,
        int TotalCount,
        int PageCount)
    {
        this.Page = Page;
        this.PageSize = PageSize;
        this.TotalCount = TotalCount;
        this.PageCount = PageCount;
        this.Items = Items;
    }

    public List<TResponse> Items { get; }

    public int Page { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

    public int PageCount { get; }

    public static async Task<PagedList<TResponse>> CreateAsync(
        IQueryable<TResponse> query, int page, int pageSize, CancellationToken token = default)
    {
        int count = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

        return new(items, page, pageSize, count, count % pageSize == 0 ? count / pageSize : count / pageSize + 1);
    }
}