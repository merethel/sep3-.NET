using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared;
using Shared.Dtos;

namespace Application.Logic;

public class EventLogic : IEventLogic
{
    private readonly IEventDao EventDao;
    private readonly ICompanyDao CompanyDao;

    public EventLogic(IEventDao eventDao, ICompanyDao companyDao)
    {
        EventDao = eventDao;
        CompanyDao = companyDao;
    }
    public async Task<Event> CreateAsync(EventCreationDto dto)
    {
        Company? owner = await CompanyDao.GetByUsernameAsync(dto.Username);
        if (owner == null)
            throw new Exception($"The Company with this username: {dto.Username} does not exist");
        
        ValidateData(dto);
        Event toCreate = new Event(owner: owner, title: dto.Title, description: dto.Description, dateTime: dto.DateTime);
        
        Event created = await EventDao.CreateAsync(toCreate);
        
        return created;
    }

    private static void ValidateData(EventCreationDto eventToCreate)
    {
        string title = eventToCreate.Title;
        string description = eventToCreate.Description;
        DateTime date = eventToCreate.DateTime;
        DateTime today = DateTime.Today; 

        if (title.Length < 3)
            throw new Exception("Title must be at least 3 characters!");

        if (title.Length > 15)
            throw new Exception("Title must be less than 16 characters!");
        if (description.Length <= 0)
        {
            throw new Exception("Description cannot be empty");
        }
        if (date.CompareTo(today) < 1)
        {
            throw new Exception("The date for your event has to be later than todays date.");
        }
    }
}