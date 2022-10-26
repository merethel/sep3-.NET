using Application.DaoInterfaces;
using Shared;

namespace FileData.DAOs;

public class CompanyFileDao : ICompanyDao
{
    private readonly FileContext context;

    public CompanyFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Company> CreateAsync(Company company)
    {
        int companyId = 1;
        if (context.Companies.Any())
        {
            companyId = context.Companies.Max(u => u.Id);
            companyId++;
        }

        company.Id = companyId;

        context.Companies.Add(company);
        context.SaveChanges();

        return Task.FromResult(company);
    }

    public Task<Company?> GetByUsernameAsync(string username)
    {
        Company? existing = context.Companies.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }
}