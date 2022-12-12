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
            exceptionString += Environment.NewLine + "Titel skal have mere end 3 tegn";
        }

        if (title.Length > 32)
        {
            exceptionString += Environment.NewLine + "Titel skal være mindre end 32 tegn";
        }

        if (description.Length <= 0)
        {
            exceptionString += Environment.NewLine + "Beskrivelsen må ikke være tom";
        }
        
        if (location.Length <= 0)
        {
            exceptionString += Environment.NewLine + "Lokation skal udfyldes";
        }
        
        if (date.CompareTo(today) < 0)
        {
            exceptionString += Environment.NewLine + "Med mindre du kan rejse i tiden, så vælg venligst en senere dato";
        }

        if (date.Year > 2100)
        {
            exceptionString += Environment.NewLine + "Du er højst sandsynlig død til den tid, tror du ikke?";
        }

        if (category.Length == 0)
        {
            exceptionString += Environment.NewLine + "Du skal vælge en kategori";
        }

        
        if (area.Length == 0)
        {
            exceptionString += Environment.NewLine + "Du skal vælge et område";
        }

        if (!exceptionString.Equals(null))
        {
            throw new Exception(exceptionString);
        }
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