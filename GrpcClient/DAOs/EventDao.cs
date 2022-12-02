

using Application.DaoInterfaces;
using GrpcClient.ClientInterfaces;
using GrpcClient.Services;
using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.DAOs;

public class EventDao : IEventDao
{
    private readonly IEventClient Service;

    public EventDao(IEventClient service)
    {
        Service = service;
    }

    public async Task<Event?> CreateAsync(EventCreationDto eventDto)
    {
        Event created = await Service.CreateAsync(eventDto);

        return created;
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        Event @event = await Service.GetByIdAsync(id);

        return @event;
    }

    public async Task<List<Event>> GetAsync()
    {
        List<Event> events = await Service.GetAsync();

        return events;
    }

    public async Task<Event?> RegisterAttendeeAsync(int userId, int eventId)
    {
        Event eventToReturn = await Service.RegisterAttendeeAsync(userId,eventId);

        return eventToReturn;
    }

    public async Task<Event?> CancelAsync(int eventId)
    {
        Event eventToReturn = await Service.CancelAsync(eventId);
        return eventToReturn;
    }
}
