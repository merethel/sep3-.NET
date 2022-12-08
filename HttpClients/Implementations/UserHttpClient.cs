﻿using System.Net.Http.Json;
using System.Text.Json;
using GrpcService1;
using HttpClients.ClientInterfaces;
using Shared;
using Shared.Dtos;
using Shared.Models;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient Client;
    public static int? userId { get; private set; } = 0;
    
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

    public async Task<int> GetUserId(string username)
    {
        HttpResponseMessage response = await Client.GetAsync("/user?username="+username);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        userId = user.Id;
        return user.Id;
    }

    public async Task<User> DeleteUser(int userId)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/user");
        requestMessage.Content = JsonContent.Create(new IntRequest
        {
            Int = userId
        });
        HttpResponseMessage response = await Client.SendAsync(requestMessage);
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