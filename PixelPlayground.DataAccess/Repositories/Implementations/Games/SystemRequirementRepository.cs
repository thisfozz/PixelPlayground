using DataAccess.Contexts;
using DataAccess.Entities.Games;
using DataAccess.Repositories.Contracts.Interfaces.Games;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations.Games;

public class SystemRequirementRepository : ISystemRequirementRepository
{
    private readonly PixelBaseContext _context;

    public SystemRequirementRepository(PixelBaseContext context)
    {
        _context = context;
    }
    public async Task<bool> SystemRequirementsExistsAsync(Guid gameId)
    {
        return await _context.SystemRequirements.AnyAsync(sr => sr.GameId == gameId);
    }

    public async Task<bool> CreateSystemRequirementsAsync(SystemRequirementEntity systemRequirement)
    {
        var existingRequirements = await _context.SystemRequirements.FirstOrDefaultAsync(sr => sr.GameId == systemRequirement.GameId);
        if (existingRequirements != null) return false;

        _context.SystemRequirements.Add(systemRequirement);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<SystemRequirementEntity?> GetSystemRequirementsByGameIdAsync(Guid gameId)
    {
        return await _context.SystemRequirements.FirstOrDefaultAsync(sr => sr.GameId == gameId);
    }

    public async Task<bool> UpdateSystemRequirementsAsync(Guid gameId, SystemRequirementEntity updatedRequirements)
    {
        var existingRequirements = await _context.SystemRequirements.FirstOrDefaultAsync(sr => sr.GameId == gameId);

        if (existingRequirements == null) return false;

        existingRequirements.MinRequirements = updatedRequirements.MinRequirements;
        existingRequirements.RecommendedRequirements = updatedRequirements.RecommendedRequirements;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteSystemRequirementsAsync(Guid gameId)
    {
        var existingRequirements = await _context.SystemRequirements.Where(req => req.GameId == gameId).ToListAsync();

        if (existingRequirements.Count == 0) return false;

        _context.SystemRequirements.RemoveRange(existingRequirements);
        return await _context.SaveChangesAsync() > 0;
    }
}