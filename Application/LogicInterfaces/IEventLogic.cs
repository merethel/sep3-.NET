using Shared;
using Shared.Dtos;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IEventLogic
{
    Task<Event> CreateAsync(EventCreationDto eventToCreate);

}