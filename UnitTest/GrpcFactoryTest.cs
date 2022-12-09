using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using GrpcClient.sServices;
using GrpcService1;
using NUnit.Framework;
using Shared.Dtos;
using Shared.Models;
using EventService = GrpcService1.EventService;
using UserService = GrpcService1.UserService;

namespace UnitTest.EventLogicTest;

public class GrpcFactoryTest
{

    private UserMessage TestUserMessage;
    private UserCreationDto testUserCreationDto;
    private EventMessage TestEventMessage;
    private EventCreationDto TestEventCreationDto;
    private EventCreationDtoMessage TestEventCreationDtoMessage;
    private ListEventMessage TestListEventMessage;

    public GrpcFactoryTest()
    {
        TestUserMessage = new UserMessage
        {
            Email = "email@email.dk",
            Id = 1,
            Password = "password",
            Role = "role",
            Username = "user",
        };

        testUserCreationDto = new UserCreationDto()
        {
            Email = "email@email.dk",
            Password = "password",
            Role = "role",
            Username = "user",
        };
        
        TestEventMessage = new EventMessage()
        {
            Area = "area",
            Category = "category",
            DateTime = new DateTimeMessage
            {
                Day = 1,
                Hour = 12,
                Min = 0,
                Month = 1,
                Year = 2024,
            },
            Description = "description",
            Location = "location",
            Title = "title",
            Id = 1,
            User = TestUserMessage
        };;

        List<EventMessage> events = new List<EventMessage>();
        events.Add(TestEventMessage);
        events.Add(TestEventMessage);
        TestListEventMessage = new ListEventMessage
        {
            Events = { events }
        };
        
        TestEventCreationDto = new EventCreationDto
        {
            Area = "area",
            Category = "category",
            DateTime = DateTime.Today.AddMonths(2),
            Description = "description",
            Location = "location",
            Title = "title",
            Username = "username"
        };;
    }

    [Test]
    public void GetUserClientReturnsUserServiceClient()
    {
        //Arrange
        //Act
        var userServiceClient = GrpcFactory.GetUserClient();

        //Assert
        Assert.True(userServiceClient is UserService.UserServiceClient);
    }
    
    [Test]
    public void GetEventClientReturnsEventServiceClient()
    {
        //Arrange
        //Act
        var eventServiceClient = GrpcFactory.GetEventClient();
        //Assert
        Assert.True(eventServiceClient is EventService.EventServiceClient);
    }
    [Test]
    public void TestFromEventCreationDtoToMessage()
    {
        //Arrange
        EventCreationDto dto = TestEventCreationDto;
        //Act
        EventCreationDtoMessage dtoMessage = GrpcFactory.FromEventCreationDtoToMessage(dto);
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(dto.Area.Equals(dtoMessage.Area));
                Assert.That(dto.Category.Equals(dtoMessage.Category));
                Assert.That(dto.DateTime.Day == dtoMessage.DateTime.Day);
                Assert.That(dto.Description.Equals(dtoMessage.Description));
                Assert.That(dto.Location.Equals(dtoMessage.Location));
                Assert.That(dto.Title.Equals(dtoMessage.Title));
                Assert.That(dto.Area.Equals(dtoMessage.Area));
                Assert.That(dto.Username.Equals(dtoMessage.Username));

            }
        );
    }
    
    [Test]
    public void TestFromMessageToEvent()
    {
        //Arrange
        EventMessage @event = TestEventMessage;
        //Act
        Event eventMessage = GrpcFactory.FromMessageToEvent(@event);
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(@event.Area.Equals(eventMessage.Area));
                Assert.That(@event.Category.Equals(eventMessage.Category));
                Assert.That(@event.DateTime.Day == eventMessage.DateTime.Day);
                Assert.That(@event.Description.Equals(eventMessage.Description));
                Assert.That(@event.Location.Equals(eventMessage.Location));
                Assert.That(@event.Title.Equals(eventMessage.Title));
                Assert.That(@event.Area.Equals(eventMessage.Area));
                Assert.That(@event.User.Username.Equals(eventMessage.Owner.Username));
            }
        );
    }
    
    [Test]
    public void TestFromDateTimeToDateTimeMessage()
    {
        //Arrange
        DateTime dateTime = new DateTime(2000,12,1,12,0,0);
        //Act
        DateTimeMessage dateTimeMessage = GrpcFactory.FromDateTimeToDateTimeMessage(dateTime);
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(dateTime.Year == dateTimeMessage.Year); 
                Assert.That(dateTime.Month == dateTimeMessage.Month); 
                Assert.That(dateTime.Day == dateTimeMessage.Day); 
                Assert.That(dateTime.Hour == dateTimeMessage.Hour); 
                Assert.That(dateTime.Minute == dateTimeMessage.Min);
            }
        );
    }

    [Test]
    public void TestFromDateTimeMessageToDateTime()
    {
        //Arrange
        DateTimeMessage dateTimeMessage = new DateTimeMessage
        {
            Day = 1,
            Month = 1,
            Year = 2024,
            Hour = 12,
            Min = 0
        };
        //Act
        DateTime dateTime = GrpcFactory.FromDateTimeMessageToDateTime(dateTimeMessage);
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(dateTime.Year == dateTimeMessage.Year); 
                Assert.That(dateTime.Month == dateTimeMessage.Month); 
                Assert.That(dateTime.Day == dateTimeMessage.Day); 
                Assert.That(dateTime.Hour == dateTimeMessage.Hour); 
                Assert.That(dateTime.Minute == dateTimeMessage.Min);
            }
        );
    }

    [Test]
    public void TestFromMessageToUser()
    {
        //Arrange
        UserMessage userMessage = TestUserMessage;
        //Act
        User user = GrpcFactory.FromMessageToUser(userMessage);
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(user.Email.Equals(userMessage.Email)); 
                Assert.That(user.Username.Equals(userMessage.Username)); 
                Assert.That(user.Password.Equals(userMessage.Password)); 
                Assert.That(user.Id == userMessage.Id); 
                Assert.That(user.Role.Equals(userMessage.Role)); 
            }
        );
    }

    [Test]
    public void TestFromUserCreationDtoToMessage()
    {
        //Arrange
        UserCreationDto dto = testUserCreationDto;
        //Act
        UserCreationDtoMessage dtoMessage = GrpcFactory.FromUserCreationDtoToMessage(dto);
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(dto.Email.Equals(dtoMessage.Email)); 
                Assert.That(dto.Username.Equals(dtoMessage.Username)); 
                Assert.That(dto.Password.Equals(dtoMessage.Password)); 
                Assert.That(dto.Role.Equals(dtoMessage.Role)); 
            }
        );
    }
    
    
    [Test]
    public void TestFromListEventMessageToList()
    {
        //Arrange
        ListEventMessage listMessage = TestListEventMessage;
        //Act
        List<Event> list = GrpcFactory.FromListEventMessageToList(listMessage);
        //Assert
        Assert.That(list.Count == listMessage.Events.Count);
    }
    
    [Test]
    public void TestFromCriteriaDtoToMessage()
    {
        //Arrange
        CriteriaDto criteriaDto = new CriteriaDto
        {
            OwnerId = 1,
            Area = "Area",
            Category = "Category"
        };
        //Act
        CriteriaDtoMessage criteriaDtoMessage = GrpcFactory.FromCriteriaDtoToMessage(criteriaDto);
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(criteriaDto.Area.Equals(criteriaDtoMessage.Area));
                Assert.That(criteriaDto.Category.Equals(criteriaDtoMessage.Category));
                Assert.That(criteriaDto.OwnerId.Equals(criteriaDtoMessage.OwnerId));
            }
            );
        }
}