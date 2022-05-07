using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Models.PagedRequest;

public class PagedRequest
{
    public PagedRequest()
    {
        RequestFilters = new RequestFilters();
    }

    [Required]
    [DefaultValue(0)]
    [Range(0, Int32.MaxValue)]
    public int PageIndex { get; set; } = 0;

    [Required]
    [DefaultValue(10)]
    [Range(5, 50)]
    public int PageSize { get; set; } = 5;

    [Required]
    [DefaultValue("id")]
    public string ColumnNameForSorting { get; set; } = String.Empty;

    [Required]
    [DefaultValue("ASC")]
    public string SortDirection { get; set; } = String.Empty;

    public RequestFilters RequestFilters { get; set; }
}