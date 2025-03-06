using EFBlazor.Models;
using Microsoft.AspNetCore.Components;

namespace EFBlazor.ComponentBases;

public class SearchComponentBase<TModel, TService, TFilter> : ComponentBase
    where TModel : class, new()
    where TService : IFilterOperations<TModel, TFilter>
    where TFilter : PagedFilter, new()
{
    [Parameter] public TFilter Filter { get; set; } = new();
    [Inject] public TService Service { get; set; } = default!;
    [Parameter, SupplyParameterFromQuery] public int Page { get; set; }
    protected PagedResponse<TModel>? Result { get; set; }

    [Parameter] public EventCallback<TModel> OnSelect { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (Page <= 0)
        {
            Page = 1;
        }

        await LoadAsync();
    }

    protected virtual async Task LoadAsync(bool navigateToPage1 = false)
    {
        await Task.Yield();




        Filter.Page = Page < 1 ? 1 : Page;

        Result = await Service.GetAsync(Filter);
    }
}
