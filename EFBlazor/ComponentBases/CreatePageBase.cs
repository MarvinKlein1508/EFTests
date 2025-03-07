using EFBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EFBlazor.ComponentBases;

public abstract class CreatePageBase<TModel, TService> : ComponentBase
    where TModel : class, new()
    where TService : class, ICreateOperation<TModel>
{
    protected EditForm? _form;
    public TModel Input { get; set; } = new();

    [Inject] public TService Service { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    protected virtual async Task SaveAsync()
    {
        await Service.CreateAsync(Input);
        NavigationManager.NavigateTo(GetListUrl());
    }

    protected virtual Task CancelAsync()
    {
        NavigationManager.NavigateTo(GetListUrl());
        return Task.CompletedTask;
    }

    protected abstract string GetListUrl();
}
