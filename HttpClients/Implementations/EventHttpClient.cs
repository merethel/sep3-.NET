using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos;
using Shared.Models;

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
        requestMessage.Headers.Add("Authorization", "Bearer " + jwt);
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

    public async Task<ICollection<Event>> GetEvents()
    {
        HttpResponseMessage response = await Client.GetAsync("/Event");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        ICollection<Event> events = JsonSerializer.Deserialize<ICollection<Event>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return events;
    }

    public async Task<Event> RegisterAttendeeAsync(int userId, int eventId)
    {
        string? jwt = JwtAuthService.Jwt;

        RegisterAttendeeDto dto = new RegisterAttendeeDto(userId, eventId);
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Patch, "/Event");
        
        //Dette bliver sendte til http endpoint
        requestMessage.Headers.Add("Authorization", "Bearer " + jwt);
        requestMessage.Content = JsonContent.Create(dto);
        HttpResponseMessage response = await Client.SendAsync(requestMessage); //den her response (allerede her) giver server error fejl 500
        //
        
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        Event eventToReturn = JsonSerializer.Deserialize<Event>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return eventToReturn;
    }

    public Task<Event> CancelAsync(int eventId)
    {
        throw new NotImplementedException();
    }
    
}