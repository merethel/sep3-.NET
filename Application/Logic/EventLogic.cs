using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared;
using Shared.Dtos;
using Shared.Models;

namespace Application.Logic;

public class EventLogic : IEventLogic
{
    private readonly IEventDao EventDao;
    private readonly IUserDao UserDao;

    public EventLogic(IEventDao eventDao, IUserDao userDao)
    {
        EventDao = eventDao;
        UserDao = userDao;
    }
    public async Task<Event> CreateAsync(EventCreationDto dto)
    {
        User? owner = await UserDao.GetByUsernameAsync(dto.Username);
        if (owner == null)
            throw new Exception($"The User with this username: {dto.Username} does not exist");
        
        ValidateData(dto);
        
        Event created = await EventDao.CreateAsync(dto);
        
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
        return EventDao.GetAsync(criteriaDto);
    }

    public async Task<Event> RegisterAttendeeAsync(int userId, int eventId)
    {
        Event eventToReturn = await EventDao.RegisterAttendeeAsync(userId,eventId);
        return eventToReturn;
    }
}