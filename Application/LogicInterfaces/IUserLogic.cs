using Shared;
using Shared.Dtos;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto userToCreate);
    Task<User> ValidateUser(string username, string password);
    Task<User> getUser(String username);
}