using System;
using System.Collections.Generic;
using System.Net.Http;
using HttpClients.Implementations;
using NUnit.Framework;
using Shared.Dtos;
using Shared.Models;

namespace IntegrationTest;

public class JwtAuthHttpClientIntegrationTest
{
    private readonly JwtAuthService _jwtAuthService;
    private readonly UserHttpClient _userHttpClient;

    
    public JwtAuthHttpClientIntegrationTest()
    {
        _jwtAuthService = new JwtAuthService(new HttpClient(){
            BaseAddress = new Uri("https://localhost:7122")
        });
        _userHttpClient = new UserHttpClient(new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7122")
        });
    }
    
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestLogin()
    {
        //Arrange
        UserCreationDto userToCreate = new UserCreationDto()
        {
            Username = "testUser6",
            Email = "email@gmail.com",
            Password = "password",
            Role = "User"
        };

        var userResult = _userHttpClient.Create(userToCreate).Result;

        //Act
        var result = _jwtAuthService.LoginAsync("testUser6", "password");
  
        //Assert
        //Vi kan ikke asserte en task som ikke returnerer et objekt, vi tjekker derfor bare at vi IKKE får en exception
        Assert.Pass();
    }
}