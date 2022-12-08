
using Application.LogicInterfaces;
using GrpcClient.ClientInterfaces;
using Shared.Dtos;
using Shared.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserGrpcClient _userGrpcClient;

    public UserLogic(IUserGrpcClient userGrpcClient)
    {
        _userGrpcClient = userGrpcClient;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await _userGrpcClient.GetByUsernameAsync(dto.Username);

        //QUICKFIX
        if (existing != null && existing.Username.Length != 0)
            throw new Exception("Username already taken!" + " (ノಠ益ಠ)ノ彡┻━┻");

        ValidateData(dto);

        User created = (await _userGrpcClient.CreateAsync(dto))!;

        return created;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await _userGrpcClient.GetByUsernameAsync(username);

        //QUICKFIX


        if (existingUser!.Username.Length == 0 || existingUser is null)

        {
            throw new Exception("Username not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password incorrect" + " (ノಠ益ಠ)ノ彡┻━┻");
        }

        return await Task.FromResult(existingUser);
    }

    public async Task<User> GetUser(string username)
    {
        User user = (await _userGrpcClient.GetByUsernameAsync(username))!;
        return user;
    }

   
    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.Username;
        string password = userToCreate.Password;
        string email = userToCreate.Email;

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

    public async Task<User> DeleteUser(int userId)
    {
        User userToReturn = await _userGrpcClient.DeleteUserAsync(userId)!;
        return userToReturn;
    }


}