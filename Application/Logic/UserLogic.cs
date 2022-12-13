
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
            throw new Exception("Brugernavn er allerede taget!" + " (ノಠ益ಠ)ノ彡┻━┻");

        ValidateData(dto);

        User created = (await _userGrpcClient.CreateAsync(dto))!;

        return created;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await _userGrpcClient.GetByUsernameAsync(username);


        if (existingUser!.Username.Length == 0 || existingUser is null)

        {
            throw new Exception("Brugernavn ikke fundet");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Kodeord er forkert" + " (ノಠ益ಠ)ノ彡┻━┻");
        }

        return await Task.FromResult(existingUser);
    }

    public async Task<User> GetUser(string username)
    {
        User user = (await _userGrpcClient.GetByUsernameAsync(username))!;
        return user;
    }

   
    private void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.Username;
        string password = userToCreate.Password;
        string email = userToCreate.Email;

        if (userName.Length < 3)
            throw new Exception("Brugernavn skal være mindst 3 tegn lang!" + " (ノಠ益ಠ)ノ彡┻━┻");

        if (userName.Length > 15)
            throw new Exception("Brugernavn skal være mindre end 16 tegn!" + " (ノಠ益ಠ)ノ彡┻━┻");
        if (password.Length < 8)
        {
            throw new Exception("Adgangskode skal være mere end 8 tegn lang!" + " (ノಠ益ಠ)ノ彡┻━┻");
        }
        if (!email.Contains("@"))
        {
            throw new Exception("Dette er ikke en korrekt e-mail!" + " (ノಠ益ಠ)ノ彡┻━┻");
        }
    }

    public async Task<User> DeleteUser(int userId)
    {
        User userToReturn = await _userGrpcClient.DeleteUserAsync(userId)!;
        return userToReturn;
    }


}