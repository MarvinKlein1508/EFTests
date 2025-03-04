namespace TestProject2.Models;
public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public Guid? ActiveDirectoryGuid { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int? SupervisorUserId { get; set; }
    public User? Supervisor { get; set; }
}
