using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using HttpClients.Implementations;
using NUnit.Framework;
using Shared.Dtos;
using Shared.Models;

namespace IntegrationTest;

public class EventHttpClientIntegrationTest
{
    private readonly EventHttpClient _eventHttpClient;

    public EventHttpClientIntegrationTest()
    {
        _eventHttpClient = new EventHttpClient(new HttpClient(){
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
            Username = "Jakob",
            Title = "title",
            Description = "description",
            Location = "location",
            DateTime = DateTime.Now.AddMonths(2)
        };
        //Act
        var result = _eventHttpClient.CreateAsync(dto).Result;

        //Assert
        Assert.AreEqual(result.Title, dto.Title);
    }
    [Test]
    public void TestGetAllEvents()
    {
        //Arrange
        
        //Act
        var result = _eventHttpClient.GetEvents().Result;

        //Assert
        Assert.IsInstanceOf<List<Event>>(result);
    }

    
    //den her test får databasen til at loop metoden på en eller anden måde?
    [Test]
    public void TestRegisterAttendee()
    {
        //Arrange
        
        //Act
        var result = _eventHttpClient.RegisterAttendeeAsync(1, 1).Result;

        //Assert
        Assert.IsInstanceOf<Event>(result);
    }
    

}