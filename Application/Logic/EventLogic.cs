using Application.LogicInterfaces;
using GrpcClient.ClientInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace Application.Logic;

public class EventLogic : IEventLogic
{
    private readonly IEventGrpcClient _eventGrpcClient;
    private readonly IUserGrpcClient _userGrpcClient;

    public EventLogic(IEventGrpcClient eventGrpcClient, IUserGrpcClient userGrpcClient)
    {
        _eventGrpcClient = eventGrpcClient;
        _userGrpcClient = userGrpcClient;
    }
    public async Task<Event> CreateAsync(EventCreationDto dto)
    {
        User? owner = await _userGrpcClient.GetByUsernameAsync(dto.Username);
        if (owner == null)
            throw new Exception($"Bruger med brugernavn: {dto.Username} eksisterer ikke");
        
        ValidateData(dto);
        
        Event created = (await _eventGrpcClient.CreateAsync(dto))!;
        
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
        string exceptionString = "";

        
        if (title.Length < 3)
        {
            exceptionString += "\nTitel skal have mere end 3 tegn";
            //throw new Exception(exceptionString);
        }

        if (title.Length > 32)
        {
            exceptionString += "\nTitel skal være mindre end 32 tegn";
            //throw new Exception(exceptionString);
        }

        if (description.Length <= 0)
        {
            exceptionString += "\nBeskrivelsen må ikke være tom";
            //throw new Exception(exceptionString);
        }
        
        if (location.Length <= 0)
        {
            exceptionString += "\nLokation skal udfyldes";
            //throw new Exception(exceptionString);
        }
        
        if (date.CompareTo(today) < 0)
        {
            exceptionString += "\nMed mindre du kan rejse i tiden, så vælg venligst en senere dato";
            //throw new Exception(exceptionString);
        }

        if (date.Year > 2100)
        {
            exceptionString += "\nDu er højst sandsynlig død til den tid, tror du ikke?";
            //throw new Exception(exceptionString);
        }

        if (category.Length == 0)
        {
            exceptionString += "\nDu skal vælge en kategori";
            //throw new Exception(exceptionString);
        }

        
        if (area.Length == 0)
        {
            exceptionString += "\nDu skal vælge et område";
            //throw new Exception(exceptionString);
        }

        throw new Exception(exceptionString);
    }
    
    public Task<List<Event>> GetAsync(CriteriaDto criteriaDto)
    {
        return _eventGrpcClient.GetAsync(criteriaDto);
    }

    public async Task<Event> RegisterAttendeeAsync(int userId, int eventId)
    {
        Event eventToReturn = await _eventGrpcClient.RegisterAttendeeAsync(userId,eventId);
        return eventToReturn;
    }

    public async Task<Event> CancelAsync(int eventId)
    {
        Event eventToReturn = (await _eventGrpcClient.CancelAsync(eventId))!;
        return eventToReturn;
    }
}