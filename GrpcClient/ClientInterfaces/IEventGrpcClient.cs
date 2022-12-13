using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.ClientInterfaces;

public interface IEventGrpcClient
{
    Task<Event?> CreateAsync(EventCreationDto eventDto);

    Task<List<Event?>> GetAsync(CriteriaDto criteriaDto);

    Task<Event> RegisterAttendeeAsync(int userId, int eventId);
    
    Task<Event> CancelAsync(int eventId);
}