using System.Net.Http.Json;
using System.Text.Json;
using GrpcService1;
using HttpClients.ClientInterfaces;
using Shared;
using Shared.Dtos;
using Shared.Models;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient _client;
    public static int? UserId { get; private set; }
    
    public UserHttpClient(HttpClient client)
    {
        _client = client;
        UserId = 0;
    }

    public async Task<User> Create(UserCreationDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/user", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }
    

    public async Task<User> DeleteUser(int userId)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/user");
        requestMessage.Content = JsonContent.Create(new IntRequest
        {
            Int = userId
        });
        HttpResponseMessage response = await _client.SendAsync(requestMessage);
        string result = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        User userToReturn = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return userToReturn;
        
    }
}