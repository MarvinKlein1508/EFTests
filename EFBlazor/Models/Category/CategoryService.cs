using Microsoft.EntityFrameworkCore;

namespace EFBlazor.Models;

public class CategoryService : IFilterOperations<Category, CategoryFilter>
{
    private readonly AppDbContext _dbContext;

    public CategoryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<Category>> GetAsync(CategoryFilter filter, CancellationToken cancellationToken = default)
    {
        IQueryable<Category> filteredItems = _dbContext.Categories;

        int totalCount = await filteredItems.CountAsync(cancellationToken);

        int skip = (filter.Page - 1) * filter.PageSize;

        // Anwendung der Paginierung
        filteredItems = filteredItems
            .OrderBy(x => x.CategoryId)
            .Skip(skip)
            .Take(filter.PageSize);


        var results = await filteredItems.ToListAsync(cancellationToken);

        var response = new PagedResponse<Category>
        {
            Items = results,
            Page = filter.Page,
            PageSize = filter.PageSize,
            Total = totalCount
        };

        return response;
    }

    public Task<Category?> GetAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
    }

    public Task<int> CreateAsync(Category category, CancellationToken cancellationToken = default)
    {
        _dbContext.Categories.Add(category);
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<int> UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        _dbContext.Categories.Update(category);
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _dbContext.Categories.FindAsync(id, cancellationToken);
        if (category != null)
        {
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
