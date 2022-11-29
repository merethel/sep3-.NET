using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Application.DaoInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace UnitTest.EventLogicTest;

public class EventDaoMock : IEventDao
{
    public Task<Event?> CreateAsync(EventCreationDto eventToCreate)
    {
        User user = new User();
        user.Username = "username";
        return Task.FromResult(new Event(1, user, eventToCreate.Title, eventToCreate.Description,
            eventToCreate.Location, eventToCreate.DateTime))!; 
        
    }

    public Task<Event?> GetByIdAsync(int id)
    {
        User user = new User();
        user.Username = "username";

        Event @event = new Event(id, user, "Title", "Description",
            "Location", DateTime.Now);

        return Task.FromResult(@event)!; 
    }

    public Task<List<Event>> GetAsync()
    {
        User user = new User();
        user.Username = "username";

        Event event1 = new Event(1, user, "Title", "Description",
            "Location", DateTime.Now.AddMonths(1));
        Event event2 = new Event(2, user, "Title", "Description",
            "Location", DateTime.Now.AddMonths(2));

        List<Event> events = new List<Event>();
        events.Add(event1);
        events.Add(event2);

        return Task.FromResult(events);
    }

    public Task<Event?> RegisterAttendeeAsync(int userId, int eventId)
    {
        throw new NotImplementedException();
    }
}