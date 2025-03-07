using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFBlazor.Models;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.ProductId);

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasPrecision(12, 4);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products);

        builder.HasData
        ([
            new Product() { ProductId = 1, Name = "Rice", Price = 3.95m, CategoryId = 1 },
            new Product() { ProductId = 2, Name = "Fish", Price = 5.99m, CategoryId = 1 },
            new Product() { ProductId = 3, Name = "T-Shirt", Price = 11.99m, CategoryId = 3 },
            new Product() { ProductId = 4, Name = "The Lord of the Rings", Price = 29.99m, CategoryId = 6 },
            new Product() { ProductId = 5, Name = "Harry Potter", Price = 29.99m, CategoryId = 6 },
            new Product() { ProductId = 6, Name = "MacBook Pro", Price = 3699.99m, CategoryId = 5 },
            new Product() { ProductId = 7, Name = "Serverfarm", Price = 9999999.99m, CategoryId = 5 },
        ]);
    }
}
