using Shared;
using Shared.Dtos;

namespace Application.LogicInterfaces;

public interface IEventLogic
{
    Task<Event> CreateAsync(EventCreationDto eventToCreate);

}