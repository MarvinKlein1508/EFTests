using EFTests.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTests;

public class UserTests : TestsBase
{
    [Test]
    public async Task CheckUsers()
    {
        using var dbContext = await _dbFactory.CreateDbContextAsync();

        User? user1 = await dbContext.Users
            .Include(x => x.Supervisor)
            .FirstOrDefaultAsync(x => x.UserId == 1);

        User? user2 = await dbContext.Users
            .Include(x => x.Supervisor)
            .FirstOrDefaultAsync(x => x.UserId == 2);

        Assert.That(user1, Is.Not.Null, "Der User mit UserId 1 sollte vorhanden sein.");
        Assert.That(user1.Supervisor, Is.Null, "Der User mit UserId 1 sollte keinen Supervisor haben.");

        Assert.That(user2, Is.Not.Null, "Der User mit UserId 2 sollte vorhanden sein.");
        Assert.That(user2.Supervisor, Is.Not.Null, "Der User mit UserId 2 sollte einen Supervisor haben.");

        Assert.Pass();
    }

    [Test]
    public async Task UpdateSupervisorFromDifferentContext()
    {
        Console.WriteLine("Create new DbContext1...");
        using var dbContext1 = await _dbFactory.CreateDbContextAsync();
        User? user1 = await dbContext1.Users
            .Include(x => x.Supervisor)
            .FirstOrDefaultAsync(x => x.UserId == 1);

        Assert.That(user1, Is.Not.Null, $"User 1 should exist in this context: {nameof(dbContext1)}");
        Console.WriteLine("DbContext: {0}; Supervisior for User 1: {1}", nameof(dbContext1), user1.Supervisor?.DisplayName ?? "<NULL>");


        Console.WriteLine("Create new DbContext2...");
        using var dbContext2 = await _dbFactory.CreateDbContextAsync();
        User? user2 = await dbContext2.Users
            .Include(x => x.Supervisor)
            .FirstOrDefaultAsync(x => x.UserId == 2);
        Assert.That(user2, Is.Not.Null, $"User 2 should exist in this context: {nameof(dbContext2)}");


        user1.SupervisorUserId = user2.UserId;
        user1.Supervisor = user2;
        Console.WriteLine("DbContext: {0}; Supervisior for User 1: {1}", nameof(dbContext1), user1.Supervisor?.DisplayName ?? "<NULL>");

        dbContext1.Entry(user1.Supervisor!).State = EntityState.Unchanged; // Existing objects should be marked as unchanged
        
        await dbContext1.SaveChangesAsync();
        Console.WriteLine("Updated object successfully!");

        Console.WriteLine("Create new DbContext3...");
        using var dbContext3 = await _dbFactory.CreateDbContextAsync();

        User? user1Updated = await dbContext3.Users
            .Include(x => x.Supervisor)
            .FirstOrDefaultAsync(x => x.UserId == 1);

        Assert.That(user1Updated, Is.Not.Null, $"User 1 should exist in this context: {nameof(dbContext3)}");
        Assert.That(user1Updated.Supervisor, Is.Not.Null, $"User 1 should have a supervisor in this context: {nameof(dbContext3)}");
        Console.WriteLine("DbContext: {0}; Supervisior for User 1: {1}", nameof(dbContext3), user1.Supervisor?.DisplayName ?? "<NULL>");


        Assert.Pass();
    }

    [Test]
    public async Task CreateNewUserWithSupervisor()
    {
        Console.WriteLine("Create new DbContext1...");


        using var dbContext1 = await _dbFactory.CreateDbContextAsync();
        User? user1 = await dbContext1.Users
            .Include(x => x.Supervisor)
            .FirstOrDefaultAsync(x => x.UserId == 1);

        Assert.That(user1, Is.Not.Null, $"User 1 should exist in this context: {nameof(dbContext1)}");

        User user = new()
        {
            Username = "MKT",
            DisplayName = "Marvin Klein Test",
            SupervisorUserId = user1.UserId,
            Supervisor = user1,
            Email = "mkt@localhost"
        };


        Console.WriteLine("Create new DbContext2...");
        using var dbContext2 = await _dbFactory.CreateDbContextAsync();
        dbContext2.Attach(user); // New objects should be attached
        await dbContext2.AddAsync(user);
        await dbContext2.SaveChangesAsync();

        Console.WriteLine("Create new DbContext3...");
        using var dbContext3 = await _dbFactory.CreateDbContextAsync();

        User? loadUser = await dbContext3.Users
            .Include(x => x.Supervisor)
            .FirstOrDefaultAsync(x => x.Email == "mkt@localhost");

        Assert.That(loadUser, Is.Not.Null, $"User {user.UserId} should exist in this context: {nameof(dbContext3)}");
        Assert.That(loadUser.Supervisor, Is.Not.Null, $"User {user.UserId} should have a supervisor in this context: {nameof(dbContext3)}");
        Console.WriteLine("DbContext: {0}; Supervisior for User 1: {1}", nameof(dbContext3), loadUser.Supervisor?.DisplayName ?? "<NULL>");

        Assert.Pass();
    }
}
