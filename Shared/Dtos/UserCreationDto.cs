namespace Shared.Dtos;

public class UserCreationDto
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Email { get; init; } = null!;

    public string Role { get; init; } = null!;

    public UserCreationDto(string username, string password, string email, string role)
    {
        Username = username;
        Password = password;
        Email = email;
        Role = role;
    }

    public UserCreationDto()
    {
    }
    
    public UserCreationDto()
    {}
}