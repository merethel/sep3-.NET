using Shared;
using Shared.Dtos;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IEventDao
{
    Task<Event?> CreateAsync(EventCreationDto eventToCreate);
    Task<Event?> GetByIdAsync(int id);
    
    Task<List<Event>> GetAsync(CriteriaDto criteriaDto);
    
    Task<Event?> RegisterAttendeeAsync(int userId, int eventId);
}