using Shared;
using Shared.Dtos;

namespace HttpClients.ClientInterfaces;

public interface ICompanyService
{
    Task<Company> Create(CompanyCreationDto dto);
}