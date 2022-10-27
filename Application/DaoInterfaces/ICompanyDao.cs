﻿using Shared;

namespace Application.DaoInterfaces;

public interface ICompanyDao
{
    Task<Company> CreateAsync(Company Company);
    Task<Company?> GetByUsernameAsync(string username);

    Task<Company?> GetByIdAsync(int id);
}