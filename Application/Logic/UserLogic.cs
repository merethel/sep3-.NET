
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared;
using Shared.Dtos;
using Shared.Models;

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
        
        //QUICKFIX
        if (existing.Username.Length != 0) 
            throw new Exception("Username already taken!" + " (ノಠ益ಠ)ノ彡┻━┻");

        ValidateData(dto);
        
        User created = await UserDao.CreateAsync(dto);
        
        return created;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await UserDao.GetByUsernameAsync(username);
            
        //QUICKFIX
        if (existingUser.Username.Length == 0)
        {
            throw new Exception("Username not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password incorrect" + " (ノಠ益ಠ)ノ彡┻━┻");
        }

        return await Task.FromResult(existingUser);
    }

    private static void ValidateData(UserCreationDto UserToCreate)
    {
        string userName = UserToCreate.Username;
        string password = UserToCreate.Password;
        string email = UserToCreate.Email;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!" + " (ノಠ益ಠ)ノ彡┻━┻");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!" + " (ノಠ益ಠ)ノ彡┻━┻");
        if (password.Length < 8)
        {
            throw new Exception("Password must be more than 8 characters!" + " (ノಠ益ಠ)ノ彡┻━┻");
        }
        if (!email.Contains("@"))
        {
            throw new Exception("This is not a valid email!" + " (ノಠ益ಠ)ノ彡┻━┻");
        }
    }
}