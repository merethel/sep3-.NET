

using Application.DaoInterfaces;
using GrpcClient.Services;
using Shared.Models;

namespace GrpcClient.DAOs;

public class EventFileDao : IEventDao
{
    private readonly EventService Service;

    public EventFileDao(EventService service)
    {
        Service = service;
    }

    public Task<Event> CreateAsync(Event @event)
    {
        int eventId = 1;
        if (Service.Events.Any())
        {
            eventId = Service.Events.Max(u => u.Id);
            eventId++;
        }

        @event.Id = eventId;

        Service.Events.Add(@event);
        Service.SaveChanges();

        return Task.FromResult(@event);
    }

    public Task<Event?> GetByIdAsync(int id)
    {
        Event? existing = Service.Events.FirstOrDefault(e =>
            e.Id == id
        );
        return Task.FromResult(existing);
    }
}
