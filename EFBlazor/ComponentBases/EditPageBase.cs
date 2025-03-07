using EFBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EFBlazor.ComponentBases;

public abstract class EditPageBase<TModel, TService, TIdentifier> : ComponentBase
    where TModel : class, new()
    where TService : class, IUpdateOperation<TModel>, IGetOperation<TModel, TIdentifier>
{
    protected EditForm? _form;
    public TModel? Input { get; set; }

    [Inject] public TService Service { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter] public virtual TIdentifier? Identifier { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Identifier is not null)
        {
            Input = await Service.GetAsync(Identifier);
        }
    }
    protected virtual async Task SaveAsync()
    {
        if (Input is null)
        {
            return;
        }

        await Service.UpdateAsync(Input);
        NavigationManager.NavigateTo(GetListUrl());
    }

    protected virtual Task CancelAsync()
    {
        NavigationManager.NavigateTo(GetListUrl());
        return Task.CompletedTask;
    }

    protected abstract string GetListUrl();
}
