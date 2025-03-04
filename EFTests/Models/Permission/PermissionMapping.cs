using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFTests.Models;

public class PermissionMapping : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.Property(x => x.Name)
            .HasMaxLength(50);

        builder.Property(x => x.Group)
            .HasMaxLength(50);

        builder.Property(x => x.Identifier)
            .HasMaxLength(50);


        Permission[] permissions =
        [
            new Permission
            {
                PermissionId = 1,
                Name = "Customer access",
                Identifier = "ACCESS_CUSTOMERS",
                Group = "CUSTOMERS"
            },
            new Permission
            {
                PermissionId = 2,
                Name = "Customer admin",
                Identifier = "ADMIN_CUSTOMERS",
                Group = "CUSTOMERS"
            },
        ];

        builder.HasData(permissions);
    }

}
