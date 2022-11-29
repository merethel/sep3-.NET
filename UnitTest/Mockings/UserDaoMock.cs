using System.Threading.Tasks;
using Application.DaoInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace UnitTest.EventLogicTest;

public class UserDaoMock : IUserDao
{
    public Task<User> CreateAsync(UserCreationDto user)
    {
        User userToCreate = new User(user.Username, user.Password, user.Email, 1);
        userToCreate.Id = 1;
        return Task.FromResult(userToCreate);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User userToCreate = new User(username, "password", "email@email.dk", 1);
        userToCreate.Id = 1;
        return Task.FromResult(userToCreate)!;    }

    public Task<User?> GetByIdAsync(int id)
    {
        User userToCreate = new User("username", "password", "email@email.dk", 1);
        userToCreate.Id = id;
        return Task.FromResult(userToCreate)!;
        
    }
}
