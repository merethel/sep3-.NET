using System.Diagnostics.Tracing;
using Application.DaoInterfaces;
using GrpcClient.ClientInterfaces;
using GrpcClient.Services;
using Shared.Dtos;
using Shared.Models;

namespace GrpcClient.DAOs;

public class UserDao : IUserDao
{
    private readonly IUserClient Service;

    public UserDao(IUserClient service)
    {
        Service = service;
    }

    public async Task<User> CreateAsync(UserCreationDto userDto)
    {
        User created = await Service.CreateAsync(userDto);

        return created;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        User user = await Service.GetByUsernameAsync(username);

        return user;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        User user = await Service.GetByIdAsync(id);

        return user;
    }
    
}