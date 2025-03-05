namespace EFBlazor.Models;

public interface IFilterOperations<TModel, TFilter>
{
    Task<PagedResponse<TModel>> GetAsync(TFilter filter, CancellationToken cancellationToken = default);   
}
