using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared;
using Shared.Dtos;

namespace HttpClients.Implementations;

public class EventHttpClient : IEventService
{
    private readonly HttpClient Client;

    public EventHttpClient(HttpClient client)
    {
        Client = client;
    }

    public async Task<Event> CreateAsync(EventCreationDto dto)
    {
        string? jwt = JwtAuthService.Jwt;
        
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Event");
        requestMessage.Headers.Add("Authorization", "bearer " + jwt);
        requestMessage.Content = JsonContent.Create(dto);
        HttpResponseMessage response = await Client.SendAsync(requestMessage);
        
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        

        Event @event = JsonSerializer.Deserialize<Event>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return @event;
    }
}