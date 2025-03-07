namespace EFBlazor.Models;

public interface IGetOperation<TModel, TIdentifier>
{
    Task<TModel?> GetAsync(TIdentifier identifier, CancellationToken cancellationToken = default);
}