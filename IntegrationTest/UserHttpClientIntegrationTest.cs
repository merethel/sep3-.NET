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
            Username = "testUser1",
            Email = "email@gmail.com",
            Password = "password"
        };
        //Act
        User user = _userHttpClient.Create(dto).Result;

        //Assert
        Assert.AreEqual(dto.Username, user.Username);
    }
    
    [Test]
    public void TestGetUserId()
    {
        //Arrange
        UserCreationDto dto = new UserCreationDto()
        {
            Username = "testUser2",
            Email = "email@gmail.com",
            Password = "password"
        };
        //Act
        User user = _userHttpClient.Create(dto).Result;
        
        
        int id = _userHttpClient.getUserId(user.Username).Result;

        //Assert
        Assert.AreEqual(user.Id, id);
    }
}