using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.ClientInterfaces;

public interface IUserGrpcClient
{
    Task<User> CreateAsync(UserCreationDto userDto);
    Task<User?> GetByUsernameAsync(string username);
    Task<User> DeleteUserAsync(int userId);
}