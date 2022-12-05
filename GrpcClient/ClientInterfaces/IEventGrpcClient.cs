using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.ClientInterfaces;

public interface IEventClient
{
    Task<Event?> CreateAsync(EventCreationDto eventDto);
    Task<Event?> GetByIdAsync(int id);
    
    Task<List<Event>> GetAsync(CriteriaDto criteriaDto);

    Task<Event> RegisterAttendeeAsync(int userId, int eventId);
    
    Task<Event?> CancelAsync(int eventId);
    
}