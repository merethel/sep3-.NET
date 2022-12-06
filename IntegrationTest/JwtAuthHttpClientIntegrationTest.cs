using System;
using System.Collections.Generic;
using System.Net.Http;
using HttpClients.Implementations;
using NUnit.Framework;
using Shared.Dtos;

namespace IntegrationTest;

public class JwtAuthHttpClientIntegrationTest
{
    private readonly JwtAuthService jwtAuthService;

    
    public JwtAuthHttpClientIntegrationTest()
    {
        jwtAuthService = new JwtAuthService(new HttpClient(){
            BaseAddress = new Uri("https://localhost:7122")
        });
    }
    
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestCreateEvent()
    {
        //Arrange
        EventCreationDto dto = new EventCreationDto()
        {
            Username = "username",
            Title = "title",
            Description = "description",
            Location = "location",
            DateTime = DateTime.Now.AddMonths(2)
        };
        //Act
        var result = jwtAuthService.LoginAsync("username", "password");

        //Assert
        Assert.AreEqual("username", JwtAuthService.Username);
    }
}