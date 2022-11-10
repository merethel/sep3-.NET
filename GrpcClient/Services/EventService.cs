using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient.ClientInterfaces;
using GrpcService1;
using Microsoft.Extensions.Logging;
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
}