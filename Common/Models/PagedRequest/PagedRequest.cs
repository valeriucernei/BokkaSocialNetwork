namespace Common.Models.PagedRequest;

public class PagedRequest
{
    public PagedRequest()
    {
        RequestFilters = new RequestFilters();
    }

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 5;

    public string ColumnNameForSorting { get; set; } = String.Empty;

    public string SortDirection { get; set; } = String.Empty;

    public RequestFilters RequestFilters { get; set; }
}