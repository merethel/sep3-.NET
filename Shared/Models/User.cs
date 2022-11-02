namespace Shared.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int SecurityLevel { get; set; }

    public User(string username, string password, string email, int securityLevel)
    {
        Username = username;
        Password = password;
        Email = email;
        SecurityLevel = securityLevel;
    }
}