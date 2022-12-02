using GrpcClient.ClientInterfaces;
using GrpcService1;
using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.Services;

public class EventService : IEventClient
{
    public async Task<Event?> CreateAsync(EventCreationDto eventDto)
    {
        var client = GrpcFactory.getEventClient();

        EventMessage reply = await client.createAsync(GrpcFactory.fromEventCreationDtoToMessage(eventDto));
        Event eventToReturn = GrpcFactory.fromMessageToEvent(reply);
        return eventToReturn;
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        var client = GrpcFactory.getEventClient();
        EventMessage replyMessage = await client.getByIdAsync(new IntRequest() {Int = id});
        Event eventToReturn = GrpcFactory.fromMessageToEvent(replyMessage);
        return eventToReturn;
    }

    public async Task<List<Event>> GetAsync(CriteriaDto criteriaDto)
    {
        var client = GrpcFactory.getEventClient();
        ListEventMessage reply = await client.getAllEventsAsync(GrpcFactory.fromCriteriaDtoToMessage(criteriaDto));
        List<Event> eventToReturn = GrpcFactory.fromListEventMessageToList(reply);
        
        return eventToReturn;
    }

    public async Task<Event> RegisterAttendeeAsync(int userId, int eventId)
    {
        var client = GrpcFactory.getEventClient();
        
        Console.WriteLine(userId + "-----" + eventId);

        EventMessage replyMessage = await client.addAttendeeToEventAttendeeListAsync(new AddAttendeeRequest()
        {
            UserId = userId,
            EventId = eventId
        });
        Event eventToReturn = GrpcFactory.fromMessageToEvent(replyMessage);
        return eventToReturn;
    }
}