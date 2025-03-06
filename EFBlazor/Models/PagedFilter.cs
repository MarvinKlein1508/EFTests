namespace EFBlazor.Models;

public class PagedFilter
{
    public string SearchPhrase { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}
