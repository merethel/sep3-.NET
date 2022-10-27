namespace Shared.Dtos;

public class CompanyLoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }

    public CompanyLoginDto(string username, string password)
    {
        Username = username;
        Password = password;
    }
}