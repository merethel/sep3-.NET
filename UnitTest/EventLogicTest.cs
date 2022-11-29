using System;
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
            SecurityLevel = 1
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
    public void CreateEventWithEmptyTitle()
    {
        User user = new User
        {
            Email = "mail@via.dk",
            Username = "username",
            Password = "password",
            SecurityLevel = 1
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
        Assert.Throws<Exception>(() => eventLogic.CreateAsync(eventToCreate));
    }
        
}