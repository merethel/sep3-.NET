

using System.Reflection;
using Application.Daos;
using Application.LogicInterfaces;
using Shared;
using Shared.Dtos;

namespace Application.Logic;

public class UserLogic : ICompanyLogic
{
    private readonly ICompanyDao userDao;

    public UserLogic(ICompanyDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<Company> CreateAsync(CompanyCreationDto dto)
    {
        Company? existing = await userDao.GetByUsernameAsync(dto.Username);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        Company toCreate = new Company(username: dto.Username, password: dto.Password, email: dto.Email);
        
        Company created = await userDao.CreateAsync(toCreate);
        
        return created;
    }

    private static void ValidateData(CompanyCreationDto CompanyToCreate)
    {
        string userName = CompanyToCreate.Username;
        string password = CompanyToCreate.Password;
        string email = CompanyToCreate.Email;

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