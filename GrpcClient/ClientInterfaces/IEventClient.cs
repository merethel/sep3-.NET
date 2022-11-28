using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.ClientInterfaces;

public interface IEventClient
{
    Task<Event?> CreateAsync(EventCreationDto eventDto);
    Task<Event?> GetByIdAsync(int id);
    
    Task<List<Event>> GetAsync();

    void RegisterAttendee(int userId, int eventId);
}