using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TestProject2.Models;
public class MyDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }


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
