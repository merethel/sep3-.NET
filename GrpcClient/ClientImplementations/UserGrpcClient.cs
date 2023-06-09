﻿using GrpcClient.ClientInterfaces;
using GrpcClient.sServices;
using GrpcService1;
using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.ClientImplementations;

public class UserGrpcService : IUserGrpcClient
{
    public async Task<User?> CreateAsync(UserCreationDto userDto)
    {
        var client = GrpcFactory.GetUserClient();

        UserMessage reply = await client.createAsync(GrpcFactory.FromUserCreationDtoToMessage(userDto));
        User userToReturn = GrpcFactory.FromMessageToUser(reply);
        return userToReturn;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var client = GrpcFactory.GetUserClient();
        UserMessage replyMessage = await client.getByUsernameAsync(new StringRequest() {String = username});
        User userToReturn = GrpcFactory.FromMessageToUser(replyMessage);
        return userToReturn;
    }

    public async Task<User?> DeleteUserAsync(int userId)
    {
        var client = GrpcFactory.GetUserClient();
        UserMessage replyMessage = await client.deleteUserAsync(new IntRequest() { Int = userId });
        User userToReturn = GrpcFactory.FromMessageToUser(replyMessage);
        return userToReturn;
    }
}