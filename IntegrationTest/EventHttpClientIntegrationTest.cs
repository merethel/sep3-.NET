using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using HttpClients.Implementations;
using NUnit.Framework;
using Shared.Dtos;
using Shared.Models;

namespace IntegrationTest;

public class EventHttpClientIntegrationTest
{
    private readonly EventHttpClient _eventHttpClient;
    private readonly UserHttpClient _userHttpClient;

    
    /*!! Da CancelAsync() er ændret fra at blive slettet i databasen, til at få en attribut ændret i stedet,
     så har vi ikke en reel metode til at slette efter vores test (de kan kun køres én gang)
     */
    
    
    public EventHttpClientIntegrationTest()
    {
        _eventHttpClient = new EventHttpClient(new HttpClient(){
            BaseAddress = new Uri("https://localhost:7122")
        });
        _userHttpClient = new UserHttpClient(new HttpClient(){
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
            DateTime = DateTime.Now.AddMonths(2),
            Category = "category",
            Area = "area"
        };
        //Act
        var result = _eventHttpClient.CreateAsync(dto).Result;

        //Assert
        Assert.AreEqual(result.Title, dto.Title);
    }
    [Test]
    public void TestGetAllEventsWithNoCriteria()
    {
        //Arrange
        
        //Act
        var result = _eventHttpClient.GetEvents(new CriteriaDto()).Result;

        //Assert
        Assert.IsInstanceOf<List<Event>>(result);
    }

    
    //den her test får databasen til at loop metoden på en eller anden måde?
    [Test]
    public void TestRegisterAttendee()
    {
        //Arrange

        EventCreationDto dto = new EventCreationDto()
        {
            Username = "Jakob",
            Title = "title",
            Description = "description",
            Location = "location",
            DateTime = DateTime.Now.AddMonths(2),
            Category = "category",
            Area = "area"
        };
        
        UserCreationDto dtoUser = new UserCreationDto()
        {
            Username = "testUser2",
            Email = "email@gmail.com",
            Password = "password",
            Role = "User"
        };
        
        Event @event = _eventHttpClient.CreateAsync(dto).Result;
        User user = _userHttpClient.Create(dtoUser).Result;

        
        //Act
        var result = _eventHttpClient.RegisterAttendeeAsync(user.Id, @event.Id).Result;
        List<User> users = result.Attendees;
        //Assert
        foreach (var u in users)
        {
            if (u.Id == user.Id)
            {
                Assert.Pass();
            }
        }
    }
    
    [Test]
    public void TestGetAllEventsWithAreaCriteria()
    {
        //Arrange

        //Act
        var result = _eventHttpClient.GetEvents(new CriteriaDto(0, null, "Jylland")).Result;
        
        //Assert
        foreach (var e in result)
        {
            Assert.True(e.Area.Equals("Jylland"));
        }
    }
    
    [Test]
    public void TestGetAllEventsWithCategoryCriteria()
    {
        //Arrange

        //Act
        var result = _eventHttpClient.GetEvents(new CriteriaDto(0, "Klima", null)).Result;
        
        //Assert
        foreach (var e in result)
        {
            Assert.True(e.Category.Equals("Klima"));
        }
    }
    
    [Test]
    public void TestGetAllEventsWithMultipleCriteria()
    {
        //Arrange

        //Act
        var result = _eventHttpClient.GetEvents(new CriteriaDto(0, "Klima", "Jylland")).Result;
        
        //Assert
        foreach (var e in result)
        {
            Assert.True(e.Area.Equals("Jylland") && e.Category.Equals("Klima"));
        }
    }
}