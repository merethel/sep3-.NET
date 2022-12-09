using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using GrpcService1;
using HttpClients.ClientInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace HttpClients.Implementations;

public class EventHttpClient : IEventService
{
    private readonly HttpClient _client;
    public Action<List<Event>> Methods;

    public EventHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<Event> CreateAsync(EventCreationDto dto)
    {
        string? jwt = JwtAuthService.Jwt;
        
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Event");
        requestMessage.Headers.Add("Authorization", "Bearer " + jwt);
        requestMessage.Content = JsonContent.Create(dto);
        HttpResponseMessage response = await _client.SendAsync(requestMessage);
        
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

    public async Task<ICollection<Event>> GetEvents(CriteriaDto criteriaDto)
    {
        string? jwt = JwtAuthService.Jwt;

        string queryString = "";
        if (criteriaDto.OwnerId != null)
            queryString += "?ownerId=" + criteriaDto.OwnerId;       
        if (criteriaDto.Area != null)
            queryString += "?area=" + criteriaDto.Area;        
        if (criteriaDto.Category != null)
            queryString += "?category=" + criteriaDto.Category;
        
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "/event" + queryString);
        requestMessage.Headers.Add("Authorization", "Bearer " + jwt);
        HttpResponseMessage response = await _client.SendAsync(requestMessage);

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
        HttpResponseMessage response = await _client.SendAsync(requestMessage); //den her response (allerede her) giver server error fejl 500
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

    public async Task<Event> CancelAsync(int eventId)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/Event");
        requestMessage.Content = JsonContent.Create(new IntRequest
        {
            Int = eventId
        });
        HttpResponseMessage response = await _client.SendAsync(requestMessage);
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

    public async Task GetCancelledEvents(int userId)
    {
        Console.WriteLine("Loop started, userId: " + userId);
        while (true)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "/Event/Cancelled/" + userId);


            HttpResponseMessage response = await _client.SendAsync(requestMessage);
            string result = await response.Content.ReadAsStringAsync();
        
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }

            List<Event> eventsToReturn = JsonSerializer.Deserialize<List<Event>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

            Console.WriteLine("I have events to return: " + eventsToReturn.Count);
            
            Methods.Invoke(eventsToReturn);
            
            await Task.Delay(1000);
        }
    }

    public void AddListener(Action<List<Event>> action)
    {
        Methods += action;
    }
}