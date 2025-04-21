namespace Restaurants.Application.Common;

public class PagedResult<T>
{
    public PagedResult(int totalCount, int pageSize, int pageNumber,
        IEnumerable<T> items)
    {
        TotalCount = totalCount;
        Items = items;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        ItemsFrom = (pageNumber - 1) * pageSize + 1;
        ItemsTo = Math.Min(ItemsFrom + pageSize - 1, totalCount);
    }

    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
    public IEnumerable<T> Items { get; set; }
}