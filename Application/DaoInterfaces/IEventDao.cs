using Shared;
using Shared.Dtos;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IEventDao
{
    Task<Event?> CreateAsync(EventCreationDto eventToCreate);
    Task<Event?> GetByIdAsync(int id);
    
    
}