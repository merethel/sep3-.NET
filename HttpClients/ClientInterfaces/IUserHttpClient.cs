using Shared;
using Shared.Dtos;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> Create(UserCreationDto dto);
    Task<int> GetUserId(String username);

    Task<User> DeleteUser(int userId); //Bliver kun brugt til integration-test
}