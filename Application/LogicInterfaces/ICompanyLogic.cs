using Shared;
using Shared.Dtos;

namespace Application.LogicInterfaces;

public interface ICompanyLogic
{
    Task<Company> CreateAsync(CompanyCreationDto companyToCreate);
}