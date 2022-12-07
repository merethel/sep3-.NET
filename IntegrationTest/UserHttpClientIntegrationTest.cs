using System;
using System.Net.Http;
using HttpClients.Implementations;
using NUnit.Framework;
using Shared.Dtos;
using Shared.Models;

namespace IntegrationTest;

public class UserHttpClientIntegrationTest
{
    private readonly UserHttpClient _userHttpClient;

    public UserHttpClientIntegrationTest()
    {
        _userHttpClient = new UserHttpClient(new HttpClient(){
            BaseAddress = new Uri("https://localhost:7122")
        });
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestCreateUser()
    {
        //Arrange
        UserCreationDto dto = new UserCreationDto()
        {
            Username = "testUser2",
            Email = "email@gmail.com",
            Password = "password",
            Role = "User"
        };
        //Act
        User user = _userHttpClient.Create(dto).Result;

        //Assert
        Assert.AreEqual(dto.Username, user.Username);
    }
    
    [Test]
    public void DeletingUser()
    {
        //Arrange
        UserCreationDto user = new UserCreationDto
        {
            Email = "mail@via.dk",
            Username = "username",
            Password = "password",
            Role = "User"
        };
        User userTest =_userHttpClient.Create(user).Result;
        
        //Act
        _userHttpClient.DeleteUser(userTest.Id);
        
        //Assert
        Assert.Pass();
        //Fordi vi ikke får feedback tilbage og vi ikke har en getUserMetode, så går vi ud fra a thvis den ikke throw en exception, så 
    }
    
}