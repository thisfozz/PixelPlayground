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
        var existingUserDetail = await _context.UserDetails.FirstOrDefaultAsync(detail => detail.UserId == userDetail.UserId);
        if (existingUserDetail == null) return false;

        if (!string.IsNullOrEmpty(userDetail.FirstName) && existingUserDetail.FirstName != userDetail.FirstName)
        {
            existingUserDetail.FirstName = userDetail.FirstName;
        }

        if (!string.IsNullOrEmpty(userDetail.LastName) && existingUserDetail.LastName != userDetail.LastName)
        {
            existingUserDetail.LastName = userDetail.LastName;
        }

        if (userDetail.Birthdate.HasValue && userDetail.Birthdate.Value != default)
        {
            existingUserDetail.Birthdate = userDetail.Birthdate.Value;
        }

        existingUserDetail.UpdatedAt = DateTime.UtcNow;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UploadAvatarAsync(Guid userId, string avatarUrl)
    {
        var existingUserDetail = await _context.UserDetails.FirstOrDefaultAsync(detail => detail.UserId == userId);
        if (existingUserDetail == null) return false;

        if (!string.IsNullOrEmpty(avatarUrl) && existingUserDetail.AvatarUrl != avatarUrl)
        {
            existingUserDetail.AvatarUrl = avatarUrl;
            existingUserDetail.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        return true;
    }
}