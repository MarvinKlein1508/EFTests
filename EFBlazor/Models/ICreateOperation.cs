namespace EFBlazor.Models;

public interface ICreateOperation<TModel>
{
    Task CreateAsync(TModel input, CancellationToken cancellationToken = default);
}
