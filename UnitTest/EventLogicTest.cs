using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Logic;
using NUnit.Framework;
using Shared.Dtos;
using Shared.Models;

namespace UnitTest.EventLogicTest;

public class EventLogicTest
{
    private EventLogic eventLogic;

    public EventLogicTest()
    {
        eventLogic = new EventLogic(new EventClientMock(), new UserClientMock());
    }

    [SetUp]
    public void Setup()
    {
    }

    
    [Test]
    public void CreateEvent()
    {
        //Arrange

        User user = new User
        {
            Email = "mail@via.dk",
            Username = "username",
            Password = "password",
            Role = "User"
        };
        EventCreationDto eventToCreate = new EventCreationDto
        {
            Title = "title",
            Description = "description",
            Location = "location",
            DateTime = DateTime.Now.AddMonths(2),
            Username = user.Username,
            Area = "area",
            Category = "category"
        };
        
        //Act
        Event eventToCheck = eventLogic.CreateAsync(eventToCreate).Result;
            
        //Assert
        Assert.True(eventToCheck.Title.Equals(eventToCreate.Title) && eventToCheck is Event);
    }
    
    //---------- Title must be 3 characters

    //ZOMBIES
    //Zero
    [Test]
    public void CreateEventWithEmptyTitleThrowsException()
    {
        User user = new User
        {
            Email = "mail@via.dk",
            Username = "username",
            Password = "password",
            Role = "User"
        };
        //Arrange
        EventCreationDto eventToCreate = new EventCreationDto
        {
            Title = "",
            Description = "description",
            Location = "location",
            DateTime = DateTime.Now.AddMonths(2),
            Username = user.Username
        };
         
        //Act
        //Assert


        Assert.Throws<AggregateException>(() =>
        {
            var result = eventLogic.CreateAsync(eventToCreate).Result;
        });
    }
      
    [Test]
    public void TitleWithMoreThan32CharThrowsException()
    {
        User user = new User
        {
            Email = "mail@via.dk",
            Username = "username",
            Password = "password",
            Role = "User"
        };
        //Arrange
        EventCreationDto eventToCreate = new EventCreationDto
        {
            Title = "012345678901234567890123456789012",
            Description = "description",
            Location = "location",
            DateTime = DateTime.Now.AddMonths(2),
            Username = user.Username
        };
         
        //Act
        //Assert


        Assert.Throws<AggregateException>(() =>
        {
            var result = eventLogic.CreateAsync(eventToCreate).Result;
        });
    }
    
    [Test]
    public void EmptyDescriptionThrowsException()
    {
        User user = new User
        {
            Email = "mail@via.dk",
            Username = "username",
            Password = "password",
            Role = "User"
        };
        //Arrange
        EventCreationDto eventToCreate = new EventCreationDto
        {
            Title = "title",
            Description = "",
            Location = "location",
            DateTime = DateTime.Now.AddMonths(2),
            Username = user.Username
        };
         
        //Act
        //Assert


        Assert.Throws<AggregateException>(() =>
        {
            var result = eventLogic.CreateAsync(eventToCreate).Result;
        });
    }
    
    [Test]
    public void EmptyLocationThrowsException()
    {
        User user = new User
        {
            Email = "mail@via.dk",
            Username = "username",
            Password = "password",
            Role = "User"
        };
        //Arrange
        EventCreationDto eventToCreate = new EventCreationDto
        {
            Title = "title",
            Description = "description",
            Location = "",
            DateTime = DateTime.Now.AddMonths(2),
            Username = user.Username
        };
         
        //Act
        //Assert


        Assert.Throws<AggregateException>(() =>
        {
            var result = eventLogic.CreateAsync(eventToCreate).Result;
        });
    }

    [Test]
    public void TestThatAttendeeListCanHaveMultipleAttendees()
    {
        //arrange
        User user = new User
        {
            Id = 1,
            Username = "username"
        };
        
        User user1 = new User
        {
            Id = 2
        };
        
        User user2 = new User
        {
            Id = 3
        };

        Event createdEvent = new Event
        {
            Id = 1,
            Owner = user,
            Title = "title",
            Description = "description",
            Location = "location",
            DateTime = DateTime.Now.AddMonths(2),
            Area = "area",
            Category = "category",
            Attendees = new List<User>
            {
                user1,
                user2
            }
        };
        
        //act
        eventLogic.RegisterAttendeeAsync(user1.Id, createdEvent.Id);
        eventLogic.RegisterAttendeeAsync(user2.Id, createdEvent.Id);

        //assert
        Assert.True(createdEvent.Attendees.Count == 2);

    }
}