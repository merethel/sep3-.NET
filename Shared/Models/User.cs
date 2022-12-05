namespace Shared.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;

    public User(string username, string password, string email, string role)
    {
        Username = username;
        Password = password;
        Email = email;
        Role = role;
    }
    public User(int id, string username, string password, string email, string role)
    {
        Username = username;
        Password = password;
        Email = email;
        Role = role;
        Id = id;
    }

    public User()
    {
    }
    
    
}