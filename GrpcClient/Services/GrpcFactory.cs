using System.Collections;
using Grpc.Net.Client;
using GrpcService1;
using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.Services;

public class GrpcFactory
{
    public static GrpcService1.EventService.EventServiceClient getEventClient()
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new GrpcService1.EventService.EventServiceClient(channel);
        return client;
    }
    public static GrpcService1.UserService.UserServiceClient getUserClient()
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new GrpcService1.UserService.UserServiceClient(channel);
        return client;
    }
    
    public static EventCreationDtoMessage fromEventCreationDtoToMessage(EventCreationDto eventToMap)
    {
        EventCreationDtoMessage eventToReturn = new EventCreationDtoMessage
        {
            Description = eventToMap.Description,
            Location = eventToMap.Location,
            Username = eventToMap.Username,
            Title = eventToMap.Title,
            DateTime = new DateTimeMessage()
            {
                Day = eventToMap.DateTime.Day,
                Month = eventToMap.DateTime.Month,
                Year = eventToMap.DateTime.Year,
                Hour = eventToMap.DateTime.Hour,
                Min = eventToMap.DateTime.Minute
            }
        };
        return eventToReturn;
    }

    public static Event fromMessageToEvent(EventMessage eventToMap)
    {
        List<User> Attendees = new List<User>();
        foreach (UserMessage attendee in eventToMap.Attendees)
        {
            Attendees.Add(fromMessageToUser(attendee));
        }
        
        Event eventToReturn = new Event
        {
            Id = eventToMap.Id,
            DateTime = fromDateTimeMessageToDateTime(eventToMap.DateTime),
            Description = eventToMap.Description,
            Location = eventToMap.Location,
            Title = eventToMap.Title,
            Owner = fromMessageToUser(eventToMap.User),
            Attendees = Attendees
        };
            
        return eventToReturn;
    }

    private static DateTime fromDateTimeMessageToDateTime(DateTimeMessage dateTime)
    {
        DateTime dateTimeToReturn = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Min, 0);

        return dateTimeToReturn;
    }

    private static DateTimeMessage fromDateTimeToDateTimeMessage(DateTime dateTime)
    {
        DateTimeMessage dateTimeToReturn = new DateTimeMessage()
        {
            Day = dateTime.Day,
            Month = dateTime.Month,
            Year = dateTime.Year,
            Hour = dateTime.Hour,
            Min = dateTime.Minute
        };

        return dateTimeToReturn;
    }

    public static User fromMessageToUser(UserMessage user)
    {
        User userToReturn = new User
        {
            Username = user.Username,
            Password = user.Password,
            Email = user.Email,
            Role = user.Role,
            Id = user.Id
        };
        return userToReturn;
    }

    public static UserCreationDtoMessage fromUserCreationDtoToMessage(UserCreationDto userToMap)
    {
        UserCreationDtoMessage userToReturn = new UserCreationDtoMessage
        {
            Username = userToMap.Username,
            Password = userToMap.Password,
            Email = userToMap.Email,
            Role = userToMap.Role
        };
        return userToReturn;
    }


    public static List<Event> fromListEventMessageToList(ListEventMessage listToMap)
    {
        List<Event> listToReturn = new List<Event>();
        foreach (EventMessage eventToMap in listToMap.Events)
        {
            listToReturn.Add(fromMessageToEvent(eventToMap));
        }

        return listToReturn;
    }
}