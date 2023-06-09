﻿using GrpcClient.ClientInterfaces;
using GrpcClient.sServices;
using GrpcService1;
using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.ClientImplementations;

public class EventGrpcService : IEventGrpcClient
{
    public async Task<Event?> CreateAsync(EventCreationDto eventDto)
    {
        var client = GrpcFactory.GetEventClient();

        EventMessage reply = await client.createAsync(GrpcFactory.FromEventCreationDtoToMessage(eventDto));
        Event eventToReturn = GrpcFactory.FromMessageToEvent(reply);
        return eventToReturn;
    }

    public async Task<List<Event>> GetAsync(CriteriaDto criteriaDto)
    {
        var client = GrpcFactory.GetEventClient();
        ListEventMessage reply = await client.getAllEventsAsync(GrpcFactory.FromCriteriaDtoToMessage(criteriaDto));
        List<Event> eventToReturn = GrpcFactory.FromListEventMessageToList(reply);
        
        return eventToReturn;
    }

    public async Task<Event> RegisterAttendeeAsync(int userId, int eventId)
    {
        var client = GrpcFactory.GetEventClient();

        EventMessage replyMessage = await client.addAttendeeToEventAttendeeListAsync(new AddAttendeeRequest()
        {
            UserId = userId,
            EventId = eventId
        });
        Event eventToReturn = GrpcFactory.FromMessageToEvent(replyMessage);
        return eventToReturn;
    }

    public async Task<Event?> CancelAsync(int eventId)
    {
        var client = GrpcFactory.GetEventClient();//stub
        EventMessage replyMessage = await client.cancelAsync(new IntRequest() {Int = eventId});
        Event eventToReturn = GrpcFactory.FromMessageToEvent(replyMessage);
        return eventToReturn;
    }
    
}