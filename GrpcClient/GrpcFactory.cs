using Grpc.Net.Client;
using GrpcService1;
using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.sServices;

public class GrpcFactory
{
    public static GrpcService1.EventService.EventServiceClient GetEventClient()
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new GrpcService1.EventService.EventServiceClient(channel);
        return client;
    }
    
    public static GrpcService1.UserService.UserServiceClient GetUserClient()
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new GrpcService1.UserService.UserServiceClient(channel);
        return client;
    }
    
    public static EventCreationDtoMessage FromEventCreationDtoToMessage(EventCreationDto eventToMap)
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
            },
            Category = eventToMap.Category,
            Area = eventToMap.Area
        };
        return eventToReturn;
    }

    public static Event FromMessageToEvent(EventMessage eventToMap)
    {
        List<User> attendees = new List<User>();
        foreach (UserMessage attendee in eventToMap.Attendees)
        {
            attendees.Add(FromMessageToUser(attendee));
        }
        
        Event eventToReturn = new Event
        {
            Id = eventToMap.Id,
            DateTime = FromDateTimeMessageToDateTime(eventToMap.DateTime),
            Description = eventToMap.Description,
            Location = eventToMap.Location,
            Title = eventToMap.Title,
            Owner = FromMessageToUser(eventToMap.User),
            Attendees = attendees,
            Category = eventToMap.Category,
            Area = eventToMap.Area
        };
            
        return eventToReturn;
    }

    public static DateTime FromDateTimeMessageToDateTime(DateTimeMessage dateTime)
    {
        DateTime dateTimeToReturn = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Min, 0);

        return dateTimeToReturn;
    }

    public static DateTimeMessage FromDateTimeToDateTimeMessage(DateTime dateTime)
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

    public static User FromMessageToUser(UserMessage user)
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

    public static UserCreationDtoMessage FromUserCreationDtoToMessage(UserCreationDto userToMap)
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


    public static List<Event> FromListEventMessageToList(ListEventMessage listToMap)
    {
        List<Event> listToReturn = new List<Event>();
        foreach (EventMessage eventToMap in listToMap.Events)
        {
            listToReturn.Add(FromMessageToEvent(eventToMap));
        }

        return listToReturn;
    }
    
    //CriteriaDTO
    public static CriteriaDtoMessage FromCriteriaDtoToMessage(CriteriaDto dto)
    {

        CriteriaDtoMessage criteriaDtoMessage = new CriteriaDtoMessage();
        if (dto.OwnerId != 0)
            criteriaDtoMessage.OwnerId = dto.OwnerId;
        if (dto.Area != null)
            criteriaDtoMessage.Area = dto.Area;
        if (dto.Category != null)
            criteriaDtoMessage.Category = dto.Category;
        if (dto.IsCancelled != null)
            criteriaDtoMessage.IsCancelled = (bool)dto.IsCancelled;
        if (dto.Attendee != null)
            criteriaDtoMessage.Attendee = (int) dto.Attendee;
        return criteriaDtoMessage;
    }
}