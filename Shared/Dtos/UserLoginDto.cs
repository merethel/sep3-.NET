namespace Shared.Dtos;

public class UserLoginDto
{
    public string Username { get; }
    public string Password { get; }

    public UserLoginDto(string username, string password)
    {
        Username = username;
        Password = password;
    }
}