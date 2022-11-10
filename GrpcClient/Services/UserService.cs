using System.Security.AccessControl;
using GrpcClient.ClientInterfaces;
using GrpcService1;
using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.Services;

public class UserService : IUserClient
{
    public async Task<User?> CreateAsync(UserCreationDto userDto)
    {
        var client = GrpcFactory.getUserClient();

        UserMessage reply = await client.createAsync(GrpcFactory.fromUserCreationDtoToMessage(userDto));
        User userToReturn = GrpcFactory.fromMessageToUser(reply);
        return userToReturn;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var client = GrpcFactory.getUserClient();
        UserMessage replyMessage = await client.getByIdAsync(new IntRequest() {Int = id});
        User userToReturn = GrpcFactory.fromMessageToUser(replyMessage);
        return userToReturn;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var client = GrpcFactory.getUserClient();
        UserMessage replyMessage = await client.getByUsernameAsync(new StringRequest() {String = username});
        User userToReturn = GrpcFactory.fromMessageToUser(replyMessage);
        return userToReturn;
    }
}