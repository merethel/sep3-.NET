using Shared.Dtos;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IEventLogic
{
    Task<Event> CreateAsync(EventCreationDto eventToCreate);
    Task<List<Event>> GetAsync(CriteriaDto criteriaDto);
    Task<Event> RegisterAttendeeAsync(int userId, int eventId);

    Task<Event> CancelAsync(int eventId);
    Task<List<Event>> GetCancelledEventsAsync(int userId);
}