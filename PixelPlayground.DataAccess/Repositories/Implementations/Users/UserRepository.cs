using DataAccess.Contexts;
using DataAccess.Entities.Users;
using DataAccess.Repositories.Contracts.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Users;

public class UserRepository : IUserRepository
{
    private readonly PixelBaseContext _context;

    public UserRepository(PixelBaseContext context)
    {
        _context = context;
    }
    public async Task<bool> UserExistsByUsernameAsync(string login)
    {
        return await _context.Users.AnyAsync(user => user.Login == login);
    }

    public async Task<bool> UserExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(user => user.Email == email);
    }
    public Task<bool> CreateUserAsync(UserEntity user)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity?> GetUserByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity?> GetUserByLoginAsync(string login)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateDisplayNameAsync(Guid userId, string displayName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateEmailAsync(Guid userId, string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePasswordAsync(Guid userId, string newPasswordHash)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddBalanceAsync(Guid userId, decimal amount)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}