namespace EFBlazor.Models;

public interface IDeleteOperation<TModel>
{
    Task DeleteAsync(TModel input, CancellationToken cancellationToken = default);
}
