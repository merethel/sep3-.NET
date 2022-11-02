using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared;
using Shared.Dtos;
using Shared.Models;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient Client;

    public UserHttpClient(HttpClient client)
    {
        Client = client;
    }

    public async Task<User> Create(UserCreationDto dto)
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync("/user", dto);
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
}