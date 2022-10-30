
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared;
using Shared.Dtos;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao UserDao;

    public UserLogic(IUserDao userDao)
    {
        UserDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await UserDao.GetByUsernameAsync(dto.Username);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User toCreate = new User(username: dto.Username, password: dto.Password, email: dto.Email, securityLevel: 2);
        
        User created = await UserDao.CreateAsync(toCreate);
        
        return created;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await UserDao.GetByUsernameAsync(username);

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password incorrect");
        }

        return await Task.FromResult(existingUser);
    }

    private static void ValidateData(UserCreationDto UserToCreate)
    {
        string userName = UserToCreate.Username;
        string password = UserToCreate.Password;
        string email = UserToCreate.Email;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        if (password.Length < 8)
        {
            throw new Exception("Password must be more than 8 characters!");
        }
        if (!email.Contains("@"))
        {
            throw new Exception("This is not a valid email!");
        }
    }
}