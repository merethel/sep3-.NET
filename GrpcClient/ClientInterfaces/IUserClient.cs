using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.ClientInterfaces;

public interface IUserClient
{
    Task<User?> CreateAsync(UserCreationDto userDto);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUsernameAsync(string username);
}