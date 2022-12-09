using Shared.Dtos;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IEventService
{
    Task<Event> CreateAsync(EventCreationDto dto);
    Task<ICollection<Event>> GetEvents(CriteriaDto criteriaDto);
    Task<Event> RegisterAttendeeAsync(int userId, int eventId);

    Task<Event> CancelAsync(int eventId);

    Task GetCancelledEvents(int userId);

    void AddListener(Action<List<Event>> action);
}