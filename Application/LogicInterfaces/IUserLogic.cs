using Shared;
using Shared.Dtos;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto userToCreate);
    Task<User> ValidateUser(string username, string password);
}