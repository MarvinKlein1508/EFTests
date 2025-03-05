using EFBlazor.Models;
using Microsoft.AspNetCore.Components;

namespace EFBlazor.ComponentBases;

public class ListPageBase<TModel, TService, TFilter> : ComponentBase
    where TModel : class, new()
    where TService : IFilterOperations<TModel, TFilter>
    where TFilter : PagedFilter, new()
{
    [Parameter] public TFilter Filter { get; set; } = new();
    [Inject] public TService Service { get; set; } = default!;
    [SupplyParameterFromQuery] public int Page { get; set; } = 1;
    protected PagedResponse<TModel>? Result { get; set; }

    protected override Task OnInitializedAsync()
    {
        return LoadAsync();
    }

    protected virtual async Task LoadAsync(bool navigateToPage1 = false)
    {
        await Task.Yield();
        

       

        Filter.Page = Page < 1 ? 1 : Page;

        Result = await Service.GetAsync(Filter);
    }
}
