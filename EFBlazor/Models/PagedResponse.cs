﻿namespace EFBlazor.Models;

public class PagedResponse<TResponse>
{
    public required IEnumerable<TResponse> Items { get; init; } = [];
    public required int PageSize { get; init; }
    public required int Page { get; init; }
    public required int Total { get; init; }
    public bool HasNextpage => Total > Page * PageSize;

    public int TotalPages => (int)Math.Ceiling((double)Total / (double)PageSize);
}
