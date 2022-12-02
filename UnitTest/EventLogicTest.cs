using System;
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
        eventLogic = new EventLogic(new EventDaoMock(), new UserDaoMock());
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
            Username = user.Username
        };
         
        //Act
        eventLogic.CreateAsync(eventToCreate);
        
        //Assert
        Assert.Pass();
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


    
    
}