using GrpcClient.ClientInterfaces;
using GrpcClient.Services;
using GrpcService1;
using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.ClientImplementations;

public class UserService : IUserClient
{
    public async Task<User?> CreateAsync(UserCreationDto userDto)
    {
        var client = GrpcFactory.GetUserClient();

        UserMessage reply = await client.createAsync(GrpcFactory.FromUserCreationDtoToMessage(userDto));
        User userToReturn = GrpcFactory.FromMessageToUser(reply);
        return userToReturn;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var client = GrpcFactory.GetUserClient();
        UserMessage replyMessage = await client.getByIdAsync(new IntRequest() {Int = id});
        User userToReturn = GrpcFactory.FromMessageToUser(replyMessage);
        return userToReturn;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var client = GrpcFactory.GetUserClient();
        UserMessage replyMessage = await client.getByUsernameAsync(new StringRequest() {String = username});
        User userToReturn = GrpcFactory.FromMessageToUser(replyMessage);
        return userToReturn;
    }
}