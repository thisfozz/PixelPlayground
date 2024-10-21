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

    public async Task<bool> RegisterUserAsync(UserEntity user)
    {
        var isUserRegistered = await _context.Users.FirstOrDefaultAsync(u => u.Login == user.Login);
        if(isUserRegistered == null) return false;

        _context.Users.Add(user);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<Guid?> GetUserIdByLoginAsync(string login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Login == login);
        if (user == null) return null;

        return user.UserId;
    }

    public async Task<UserEntity?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<UserEntity?> GetUserByLoginAsync(string login)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<bool> UpdateDisplayNameAsync(Guid userId, string displayName)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.Email = displayName;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdatePasswordAsync(Guid userId, string newPasswordHash)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.PasswordHash = newPasswordHash;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateEmailAsync(Guid userId, string email)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.Email = email;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateUserRoleAsync(Guid userId, Guid roleId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        var role = await _context.Roles.FindAsync(roleId);
        if (role == null) return false;

        user.RoleId = roleId;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddBalanceAsync(Guid userId, decimal amount)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.Balance += amount;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeductBalanceAsync(Guid userId, decimal amount)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        if (user.Balance < amount) return false;

        user.Balance -= amount;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.IsDeleted = true;
        return await _context.SaveChangesAsync() > 0;
    }
}