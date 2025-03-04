using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestProject2.Models;

// Konfiguration für die Benutzer-Tabelle
public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(x => x.Username)
            .HasMaxLength(50);

        builder.Property(x => x.DisplayName)
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .HasMaxLength(255)
            .HasDefaultValue(string.Empty);

        User[] users =
        [
            new User
            {
                UserId = 1,
                Username = "admin",
                DisplayName = "Administrator",
                Email = "admin@localhost"
            },
            new User
            {
                UserId = 2,
                Username = "test",
                DisplayName = "Testuser",
                Email = "test@localhost",
                SupervisorUserId = 1
            },
            new User
            {
                UserId = 3,
                Username = "mk",
                DisplayName = "Marvin Klein",
                Email = "test@localhost"
            }
        ];
        

        builder.HasData(users);
    }

}
