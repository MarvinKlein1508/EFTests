using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFBlazor.Models;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.CategoryId);

        builder.HasData(
        [
            new Category() { CategoryId = 1, Name = "Food" },
            new Category() { CategoryId = 2, Name = "Toys" },
            new Category() { CategoryId = 3, Name = "Clothing" },
            new Category() { CategoryId = 4, Name = "Pets" },
            new Category() { CategoryId = 5, Name = "Computer" },
            new Category() { CategoryId = 6, Name = "Books" },
        ]);
    }

}
