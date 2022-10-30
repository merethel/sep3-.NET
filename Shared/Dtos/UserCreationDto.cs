﻿namespace Shared.Dtos;

public class UserCreationDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public UserCreationDto(string username, string password, string email)
    {
        Username = username;
        Password = password;
        Email = email;
    }
}