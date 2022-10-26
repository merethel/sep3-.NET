namespace Shared.Dtos;

public class CompanyCreationDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public CompanyCreationDto(string username, string password, string email)
    {
        Username = username;
        Password = password;
        Email = email;
    }
}