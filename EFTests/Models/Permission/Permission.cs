namespace EFTests.Models;
public class Permission
{
    public int PermissionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public List<RolePermission> RolePermissions { get; set; } = [];
}
