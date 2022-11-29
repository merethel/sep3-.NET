using Shared.Dtos;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IEventService
{
    Task<Event> CreateAsync(EventCreationDto dto);
    Task<ICollection<Event>> GetEvents();
    Task<Event> RegisterAttendeeAsync(int userId, int eventId);
}