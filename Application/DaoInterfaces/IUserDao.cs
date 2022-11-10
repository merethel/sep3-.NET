using Shared;
using Shared.Dtos;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(UserCreationDto User);
    Task<User?> GetByUsernameAsync(string username);

    Task<User?> GetByIdAsync(int id);
}