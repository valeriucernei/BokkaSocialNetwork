using System.Collections.ObjectModel;

namespace Common.Models.PagedRequest;

public class PaginatedResult<T> where T : class
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public IList<T> Items { get; set; } = new Collection<T>();
}