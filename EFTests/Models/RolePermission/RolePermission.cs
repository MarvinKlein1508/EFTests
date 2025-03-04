namespace EFTests.Models;
public class RolePermission
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
    public bool IsActive { get; set; }
    public Role Role { get; set; } = default!;
    public Permission Permission { get; set; } = default!;
}
