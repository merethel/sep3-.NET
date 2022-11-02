using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.Services;

public class EventService
{
    public async void CreateAsync(EventCreationDto eventDto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:9090");
        var client = new Event.EventClient(channel);

        var reply = await client.createEventAsync(
            new CreateEventRequest
            {
                Username = eventDto.Username,
                Date = eventDto.DateTime.ToShortDateString(),
                Description = eventDto.Description,
                Location = eventDto.Location,
                Time = eventDto.DateTime.ToShortTimeString(),
                Title = eventDto.Title
            });
    }
}