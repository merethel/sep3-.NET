using Shared;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User User);
    Task<User?> GetByUsernameAsync(string username);

    Task<User?> GetByIdAsync(int id);
}