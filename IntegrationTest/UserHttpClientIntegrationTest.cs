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


    [Test]
    public void TestCreateUser()
    {
        //Arrange
        UserCreationDto dto = new UserCreationDto()
        {
            Username = "newTestUser120",
            Email = "email@gmail.com",
            Password = "password",
            Role = "User"
        };

        //Act
        
        User userToCreate = _userHttpClient.Create(dto).Result;

        //Assert
        Assert.AreEqual(userToCreate.Username, dto.Username);
        _userHttpClient.DeleteUser(userToCreate.Id);
    }
    
    [Test]
    public void DeletingUser()
    {
        //Arrange
        bool thrown = false;
        UserCreationDto dto = new UserCreationDto()
        {
            Username = "newTestUser110",
            Email = "email@gmail.com",
            Password = "password",
            Role = "User"
        };

        User userToCreate = _userHttpClient.Create(dto).Result;
        
        //Act
        User userDeleted = _userHttpClient.DeleteUser(userToCreate.Id).Result;
        
        //Assert
        Assert.True(userDeleted.Id == userToCreate.Id);
    }
    
}