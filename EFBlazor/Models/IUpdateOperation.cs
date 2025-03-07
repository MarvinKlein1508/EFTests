namespace EFBlazor.Models;

public interface IUpdateOperation<TModel>
{
    Task UpdateAsync(TModel input, CancellationToken cancellationToken = default);
}
