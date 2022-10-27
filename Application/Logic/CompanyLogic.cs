
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared;
using Shared.Dtos;

namespace Application.Logic;

public class CompanyLogic : ICompanyLogic
{
    private readonly ICompanyDao CompanyDao;

    public CompanyLogic(ICompanyDao companyDao)
    {
        CompanyDao = companyDao;
    }

    public async Task<Company> CreateAsync(CompanyCreationDto dto)
    {
        Company? existing = await CompanyDao.GetByUsernameAsync(dto.Username);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        Company toCreate = new Company(username: dto.Username, password: dto.Password, email: dto.Email, securityLevel: 2);
        
        Company created = await CompanyDao.CreateAsync(toCreate);
        
        return created;
    }

    public async Task<Company> ValidateCompany(string username, string password)
    {
        Company? existingCompany = await CompanyDao.GetByUsernameAsync(username);

        if (existingCompany == null)
        {
            throw new Exception("Company not found");
        }

        if (!existingCompany.Password.Equals(password))
        {
            throw new Exception("Password incorrect");
        }

        return await Task.FromResult(existingCompany);
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