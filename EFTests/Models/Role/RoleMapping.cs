using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFTests.Models;

public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.Property(x => x.Name)
            .HasMaxLength(50);

        builder.HasData
        ([
            new() { RoleId = 1, Name = "Administrator" },
            new() { RoleId = 2, Name = "Marketing" },
            new() { RoleId = 3, Name = "Sales" },
        ]);
    }

}

