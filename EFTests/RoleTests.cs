using EFTests.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace EFTests;

public class RoleTests : TestsBase
{
    [Test]
    public async Task CheckRoles()
    {
        using var dbContext = await _dbFactory.CreateDbContextAsync();

        var role1 = await dbContext.GetRoleById(1);

        var role2 = await dbContext.GetRoleById(2);

        Assert.That(role1, Is.Not.Null, "Role 1 needs to exist.");
        Assert.That(role2, Is.Not.Null, "Role 2 needs to exist.");


        Assert.Pass();
    }

    [TestCase("Test role")]
    public async Task CreateNewRole(string rolename)
    {
        Console.WriteLine("Create new DbContext1...");
        using var dbContext1 = await _dbFactory.CreateDbContextAsync();

        var permissions = await dbContext1.Permissions.ToArrayAsync();

        Role role = new()
        {
            Name = rolename,
        };

        for (int i = 0; i < permissions.Length; i++)
        {
            var permission = permissions[i];
            role.RolePermissions.Add(new RolePermission
            {
                Role = role,
                PermissionId = permission.PermissionId,
                Permission = permission,
                IsActive = i % 2 == 0
            });
        }

        Console.WriteLine("Create new DbContext2...");
        using var dbContext2 = await _dbFactory.CreateDbContextAsync();
        dbContext2.Attach(role);
        await dbContext2.SaveChangesAsync();
        int newRoleId = role.RoleId;
        Console.WriteLine("Role {0} has been successfully created.", newRoleId);
        
        Console.WriteLine("Create new DbContext3...");
        using var dbContext3 = await _dbFactory.CreateDbContextAsync();
        var newRole = await dbContext3.GetRoleById(newRoleId);
        Assert.That(newRole, Is.Not.Null, $"Role {newRoleId} should exist in this context: {nameof(dbContext3)}");

        newRole.PrintToConsole();

        Assert.Pass();
    }

    /// <summary>
    /// This method is simulation the behaviour from blazor
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task UpdateExistingRole()
    {
        // Base class is loading the class from the database which the user wants to modify
        Console.WriteLine("Create new DbContext1...");
        using var dbContext1 = await _dbFactory.CreateDbContextAsync();

        Role? role = await dbContext1.GetRoleById(3);
        Assert.That(role, Is.Not.Null, "Role 3 needs to exist.");

        role.PrintToConsole();

        // User changes the properties with instances from different dbContext classes
        Console.WriteLine("Create new DbContext2...");
        using var dbContext2 = await _dbFactory.CreateDbContextAsync();

        var missingPermissions = await dbContext2.Permissions
                    .Where(permission => !role.RolePermissions.Select(rp => rp.PermissionId).Contains(permission.PermissionId))
                    .ToArrayAsync();

        Assert.That(missingPermissions, Is.Not.Empty, $"No permissions are missing for role 3 in context: {nameof(dbContext2)}");

        foreach (var permission in missingPermissions)
        {
            role.RolePermissions.Add(new RolePermission
            {
                RoleId = role.RoleId,
                Role = role,
                PermissionId = permission.PermissionId,
                Permission = permission,
                IsActive = false
            });
        }

        // Base class saves the entity with new DbContext
        Console.WriteLine("Create new DbContext3...");
        using var dbContext3 = await _dbFactory.CreateDbContextAsync();
        var entry = dbContext3.Entry(role);
        
        dbContext3.Attach(role);
        var entry2 = dbContext3.Entry(role);
        await dbContext3.SaveChangesAsync();

        Assert.Pass();
    }
}
