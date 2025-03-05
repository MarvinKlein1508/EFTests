using Microsoft.EntityFrameworkCore;

namespace EFBlazor.Models;

public class ProductService
{
    private readonly AppDbContext _dbContext;

    public ProductService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Product>> GetAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Products.ToListAsync(cancellationToken);
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
