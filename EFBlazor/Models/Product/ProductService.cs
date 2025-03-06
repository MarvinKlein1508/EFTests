using Microsoft.EntityFrameworkCore;

namespace EFBlazor.Models;

public class ProductService : IFilterOperations<Product, ProductFilter>
{
    private readonly AppDbContext _dbContext;

    public ProductService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<Product>> GetAsync(ProductFilter filter, CancellationToken cancellationToken = default)
    {
        IQueryable<Product> filteredItems = _dbContext.Products
            .Include(x => x.Category);

        int totalCount = await filteredItems.CountAsync(cancellationToken);

        int skip = (filter.Page - 1) * filter.PageSize;

        // Anwendung der Paginierung
        filteredItems = filteredItems
            .OrderBy(x => x.ProductId)
            .Skip(skip)
            .Take(filter.PageSize);


        var results = await filteredItems.ToListAsync(cancellationToken);

        var response = new PagedResponse<Product>
        {
            Items = results,
            Page = filter.Page,
            PageSize = filter.PageSize,
            Total = totalCount
        };

        return response;
    }
    public Task<Product?> GetAsync(int productId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Products
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
    }

    public Task<int> CreateAsync(Product input, CancellationToken cancellationToken = default)
    {
        _dbContext.Products.Add(input);
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<int> UpdateAsync(Product input, CancellationToken cancellationToken = default)
    {
        _dbContext.Products.Update(input);
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
