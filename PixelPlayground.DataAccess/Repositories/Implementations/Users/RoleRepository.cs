using DataAccess.Contexts;
using DataAccess.Entities.Users;
using DataAccess.Repositories.Contracts.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Users;

public class RoleRepository : IRoleRepository
{
    private readonly PixelBaseContext _context;

    public RoleRepository(PixelBaseContext context)
    {
        _context = context;
    }
    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _context.Roles.AnyAsync(role => role.RoleName == roleName);
    }
    public async Task<bool> CreateRoleAsync(string roleName)
    {
        if (await RoleExistsAsync(roleName)) return false;

        var newRole = new RoleEntity
        {
            RoleId = Guid.NewGuid(),
            RoleName = roleName
        };

        _context.Roles.Add(newRole);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<RoleEntity>> GetAllRolesAsync()
    {
        return await _context.Roles.OrderBy(role => role.RoleName).ToListAsync();
    }

    public async Task<RoleEntity?> GetRoleByIdAsync(Guid roleId)
    {
        return await _context.Roles.FindAsync(roleId);
    }

    public async Task<Guid?> GetIdRoleByNameAsync(string roleName)
    {
        var existingRole = await _context.Roles.FirstOrDefaultAsync(role => role.RoleName == roleName);

        return existingRole?.RoleId;
    }

    public async Task<bool> UpdateRoleAsync(Guid roleId, string newRoleName)
    {
        var existingRole = await _context.Roles.FindAsync(roleId);
        if (existingRole == null || existingRole.RoleName == newRoleName) return false;

        existingRole.RoleName = newRoleName;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteRoleAsync(Guid roleId)
    {
        var existingRole = await _context.Roles.FindAsync(roleId);

        if (existingRole == null) return false;

        _context.Roles.Remove(existingRole);
        return await _context.SaveChangesAsync() > 0;
    }
}