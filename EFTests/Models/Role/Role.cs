namespace EFTests.Models;

public class Role
{
    public int RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<RolePermission> RolePermissions { get; set; } = [];

    public void PrintToConsole()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Role:");
        Console.WriteLine($"  ID: {RoleId}");
        Console.WriteLine($"  Name: {Name}");
        Console.WriteLine("Permissions:");

        foreach (var rp in RolePermissions)
        {
            Console.WriteLine($"  - Permission ID: {rp.PermissionId}, Name: {rp.Permission?.Name ?? "<NULL>"}, Active: {rp.IsActive}");
        }
        Console.WriteLine("====================================");
    }
}

