using Application.LogicInterfaces;
using GrpcClient.ClientInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace Application.Logic;

public class EventLogic : IEventLogic
{
    private readonly IEventClient _eventClient;
    private readonly IUserClient _userClient;

    public EventLogic(IEventClient eventClient, IUserClient userClient)
    {
        _eventClient = eventClient;
        _userClient = userClient;
    }
    public async Task<Event> CreateAsync(EventCreationDto dto)
    {
        User? owner = await _userClient.GetByUsernameAsync(dto.Username);
        if (owner == null)
            throw new Exception($"The User with this username: {dto.Username} does not exist");
        
        ValidateData(dto);
        
        Event created = (await _eventClient.CreateAsync(dto))!;
        
        return created;
    }

    private static void ValidateData(EventCreationDto eventToCreate)
    {
        string title = eventToCreate.Title;
        string description = eventToCreate.Description;
        string location = eventToCreate.Location;
        DateTime date = eventToCreate.DateTime;
        DateTime today = DateTime.Today;
        string category = eventToCreate.Category;
        string area = eventToCreate.Area;

        if (title.Length < 3)
            throw new Exception("Title must be at least 3 characters!");

        if (title.Length > 32)
            throw new Exception("Title must be less than 32 characters!");
        if (description.Length <= 0)
        {
            throw new Exception("Description cannot be empty");
        }
        
        if (location.Length <= 0)
        {
            throw new Exception("Location cannot be empty");
        }
        
        if (date.CompareTo(today) < 0)
        {
            throw new Exception("Unless you can time travel please pick a later date.");
        }

        if (date.Year > 2100)
            throw new Exception("You are probably gonna be dead by then dont you think?");
        

        if (description.Length <= 0)
        {
            throw new Exception("Description cannot be empty");
        }
        
        if (category.Length == 0)
        {
            throw new Exception("You must choose a category");
        }

        
        if (area.Length == 0)
        {
            throw new Exception("You must choose an area for your event");
        }
    }
    
    public Task<List<Event>> GetAsync(CriteriaDto criteriaDto)
    {
        return _eventClient.GetAsync(criteriaDto);
    }

    public async Task<Event> RegisterAttendeeAsync(int userId, int eventId)
    {
        Event eventToReturn = await _eventClient.RegisterAttendeeAsync(userId,eventId);
        return eventToReturn;
    }

    public async Task<Event> CancelAsync(int eventId)
    {
        Event eventToReturn = (await _eventClient.CancelAsync(eventId))!;
        return eventToReturn;
    }
}