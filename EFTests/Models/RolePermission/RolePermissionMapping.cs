using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFTests.Models;

public class RolePermissionMapping : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");

        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

        builder.Property(rp => rp.IsActive)
             .IsRequired();

        builder.HasOne(rp => rp.Role)
                      .WithMany(r => r.RolePermissions)
                      .HasForeignKey(rp => rp.RoleId);

        builder.HasOne(rp => rp.Permission)
              .WithMany(p => p.RolePermissions)
              .HasForeignKey(rp => rp.PermissionId);

        builder.HasData
        ([
            new() {  RoleId = 1, PermissionId = 1, IsActive = true },
            new() {  RoleId = 1, PermissionId = 2, IsActive = true },
            new() {  RoleId = 2, PermissionId = 1, IsActive = true },
            new() {  RoleId = 2, PermissionId = 2, IsActive = false },
            new() {  RoleId = 3, PermissionId = 1, IsActive = false },
        ]);
    }
}
