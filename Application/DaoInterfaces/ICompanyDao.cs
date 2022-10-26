using Shared;

namespace Application.Daos;

public interface ICompanyDao
{
    Task<Company> CreateAsync(Company Company);
    Task<Company?> GetByUsernameAsync(string username);
}