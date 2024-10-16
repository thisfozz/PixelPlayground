using DataAccess.Contexts;
using DataAccess.Entities.Users;
using DataAccess.Repositories.Contracts.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Users;

public class UserDetailRepository : IUserDetailRepository
{
    private readonly PixelBaseContext _context;

    public UserDetailRepository(PixelBaseContext context)
    {
        _context = context;
    }

    public async Task<UserDetailEntity?> GetUserDetailByUserIdAsync(Guid userId)
    {
        return await _context.UserDetails.FirstOrDefaultAsync(user => user.UserId == userId);
    }

    public async Task<bool> UpdateUserDetailAsync(UserDetailEntity userDetail)
    {
        var existingUser = await _context.Users
            .Include(user => user.UserDetails)
            .FirstOrDefaultAsync(user => user.UserId == userDetail.UserId);

        if (existingUser == null)
        {
            return false;
        }

        var existingUserDetail = existingUser.UserDetails.FirstOrDefault();

        if (existingUserDetail == null)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(userDetail.FirstName))
        {
            existingUserDetail.FirstName = userDetail.FirstName;
        }

        if(!string.IsNullOrEmpty(userDetail.LastName))
        {
            existingUserDetail.LastName = userDetail.LastName;
        }

        if (userDetail.Birthdate.HasValue && userDetail.Birthdate.Value != default)
        {
            existingUserDetail.Birthdate = userDetail.Birthdate.Value;
        }

        existingUserDetail.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UploadAvatarAsync(UserDetailEntity userDetail, string avatarUrl)
    {
        var existingUser = await _context.Users
            .Include(user => user.UserDetails)
            .FirstOrDefaultAsync(user => user.UserId == userDetail.UserId);

        if (existingUser == null)
        {
            return false;
        }

        var existingUserDetail = existingUser.UserDetails.FirstOrDefault();

        if (existingUserDetail == null)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(avatarUrl))
        {
            existingUserDetail.AvatarUrl = avatarUrl;
        }

        existingUserDetail.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}