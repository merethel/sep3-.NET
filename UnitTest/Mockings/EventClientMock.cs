using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrpcClient.ClientInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace UnitTest.Mockings;

public class EventClientMock : IEventClient
{
    public Task<Event?> CreateAsync(EventCreationDto eventToCreate)
    {
        User user = new User();
        user.Username = "username";
        Event eventToReturn = new Event(user, eventToCreate.Title, eventToCreate.Description,
            eventToCreate.Location, eventToCreate.DateTime,eventToCreate.Category, eventToCreate.Area, new List<User>())
        {
            Id = 1
        };
        return Task.FromResult<Event?>(eventToReturn);
    }

    public Task<Event?> GetByIdAsync(int id)
    {
        User user = new User();
        user.Username = "username";

        Event @event = new Event(user, "Title", "Description",
            "Location", DateTime.Now,"category", "area", new List<User>())
        {
            Id = 1
        };
        return Task.FromResult(@event)!; 
    }

    public Task<List<Event>> GetAsync(CriteriaDto criteriaDto)
    {
        User user = new User();
        user.Username = "username";

        Event event1 = new Event(user, "Title", "Description",
            "Location", DateTime.Now.AddMonths(1), "category", "area", new List<User>())
        {
            Id = 1
        };
        Event event2 = new Event(user, "Title", "Description",
            "Location", DateTime.Now.AddMonths(2), "category", "area",new List<User>())
        {
            Id = 1
        };

        List<Event> events = new List<Event>
        {
            event1,
            event2
        };

        return Task.FromResult(events);
    }

    //Disse metoder sender bare data videre, og behøver derfor ikke testes
    public Task<Event> RegisterAttendeeAsync(int userId, int eventId)
    {
        throw new NotImplementedException();
    }

    public Task<Event?> CancelAsync(int eventId)
    {
        throw new NotImplementedException();
    }
}