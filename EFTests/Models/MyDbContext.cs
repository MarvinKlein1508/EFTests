using EFTests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFTests.Models;
public class MyDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }


    public Task<Role?> GetRoleById(int roleId) => Roles
            .Include(x => x.RolePermissions)
            .ThenInclude(x => x.Permission)
            .FirstOrDefaultAsync(x => x.RoleId == roleId);

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {

        var noLoggingFactory = LoggerFactory.Create(builder => { });
        options.UseLoggerFactory(noLoggingFactory) 
               .LogTo(_ => { }, LogLevel.None);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);
    }


}
