using Shared;
using Shared.Dtos;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IEventLogic
{
    Task<Event> CreateAsync(EventCreationDto eventToCreate);
    Task<List<Event>> GetAsync();
    Task<Event> RegisterAttendeeAsync(int userId, int eventId);

    Task<Event> CancelAsync(int eventId);
}