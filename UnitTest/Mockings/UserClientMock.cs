using System.Threading.Tasks;
using GrpcClient.ClientInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace UnitTest.Mockings;

public class UserGrpcClientMock : IUserGrpcClient
{
    public Task<User> CreateAsync(UserCreationDto user)
    {
        User userToCreate = new User(user.Username, user.Password, user.Email, "User");
        userToCreate.Id = 1;
        return Task.FromResult(userToCreate);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User userToCreate = new User(username, "password", "email@email.dk", "User");
        userToCreate.Id = 1;
        return Task.FromResult(userToCreate)!;    }

    public Task<User?> GetByIdAsync(int id)
    {
        User userToCreate = new User("username", "password", "email@email.dk", "User");
        userToCreate.Id = id;
        return Task.FromResult(userToCreate)!;
        
    }
}
