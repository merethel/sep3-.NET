using Application.DaoInterfaces;
using Shared;
using Shared.Models;

namespace FileData.DAOs;

public class EventFileDao : IEventDao
{
    private readonly FileContext Context;

    public EventFileDao(FileContext context)
    {
        Context = context;
    }

    public Task<Event> CreateAsync(Event @event)
    {
        int eventId = 1;
        if (Context.Events.Any())
        {
            eventId = Context.Events.Max(u => u.Id);
            eventId++;
        }

        @event.Id = eventId;

        Context.Events.Add(@event);
        Context.SaveChanges();

        return Task.FromResult(@event);
    }

    public Task<Event?> GetByIdAsync(int id)
    {
        Event? existing = Context.Events.FirstOrDefault(e =>
            e.Id == id
        );
        return Task.FromResult(existing);
    }
}
