using Shared;

namespace Application.DaoInterfaces;

public interface IEventDao
{
    Task<Event> CreateAsync(Event @event);
    Task<Event?> GetByIdAsync(int id);
}