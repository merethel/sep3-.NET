using Application.DaoInterfaces;
using Shared;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Companies.Any())
        {
            userId = context.Companies.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;

        context.Companies.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? existing = context.Companies.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User? existing = context.Companies.First(c => c.Id == id);
        return Task.FromResult(existing)!;

    }
}